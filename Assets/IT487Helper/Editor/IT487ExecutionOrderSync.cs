/* USAGE : Place this script into an "Editor" folder. Every new script will receive an explicit execution ordering upon creation. */

/* PURPOSE : Unity's Script Execution Ordering system is a humongous source of recurring bugs (especially for beginner devs) */
/* This script, once imported, should prevent these bugs from arising by assigning an explicit execution order index */
/* to all new scripts as they enter the project */

using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[InitializeOnLoad]
class IT487ExecutionOrderSync
{
    const bool debug_mode = false; // Turn on for lots of console logging.

    static void ArborLog(string msg)
    {
        if (debug_mode && (Application.isEditor || Debug.isDebugBuild))
            Debug.Log(msg);
    }

    /* Whenever new code (or changed code) enters the project, check the monoscripts and their exec order indices. */
    [UnityEditor.Callbacks.DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Refresh();
    }

    static void Refresh()
    {
        if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            SyncronizeExecutionOrderBetweenEditorAndBuilds();
    }

    /* Create explicit execution ordering for all scripts...
     * (1) that don't already have a DefaultExecutionOrder attribute...
     * (2) that don't already have a specified execution order index.
     */
    [MenuItem("IT 487 Helper/Force Sync (Editor - Build Execution Order)")]
    public static void SyncronizeExecutionOrderBetweenEditorAndBuilds()
    {
        /* Ensure the editor doesn't run this script twice simultaneously...
         * Or in an infinitely-looping fashion (MonoImporter.SetExecutionOrder causes script reload). */
        if (EditorPrefs.HasKey("syncing_execution_order_operations_remaining") && EditorPrefs.GetInt("syncing_execution_order_operations_remaining") > 0)
        {
            int remaining_count = EditorPrefs.GetInt("syncing_execution_order_operations_remaining");
            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", remaining_count - 1);
            return;
        }

        EditorApplication.LockReloadAssemblies();

        try
        {
            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Beginning syncronization attempt. Contact ayarger@umich.edu if issues arise.");

            /* Locate all monobehaviors */
            List<MonoScript> mono_scripts = new List<MonoScript>(MonoImporter.GetAllRuntimeMonoScripts());

            /* Sort them by name */
            int compare(MonoScript a, MonoScript b)
            {
                return a.name.CompareTo(b.name);
            }
            mono_scripts.Sort(compare);
            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Identified " + mono_scripts.Count + " total Monobehaviors in codebase.");

            /* Remove monoscripts that don't have a valid type (unclear why this is possible, but it is). */
            for (int i = mono_scripts.Count - 1; i > -1; i--)
            {
                MonoScript mono_script = mono_scripts[i];

                System.Type monobehavior_type = mono_script.GetClass();
                if (monobehavior_type == null)
                {
                    mono_scripts.RemoveAt(i);
                    continue;
                }

                /* Remove monoscripts that aren't part of the user's Assembly-CSharp assembly */
                Assembly assem = Assembly.GetAssembly(monobehavior_type);
                if (!assem.FullName.Contains("Assembly-CSharp"))
                {
                    mono_scripts.RemoveAt(i);
                }
            }

            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Identifying developer-configured (thus unavailable) indices...");
            ScriptExecutionTakenIndexResult taken_indices_result = GetUnavailableScriptExecutionIndices(mono_scripts);

            /* Remove all mono_scripts that were customized by the developer (IE, don't have an index of 0). */
            foreach (MonoScript mono_script in taken_indices_result.customized_monoscripts)
            {
                if (mono_scripts.Contains(mono_script))
                    mono_scripts.Remove(mono_script);
            }

            /* Adjust execution order */
            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", mono_scripts.Count);
            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Adjusting script execution orders (" + mono_scripts.Count + " need adjusting)...");
            int order_index = 1;
            foreach (MonoScript monoscript in mono_scripts)
            {
                while (taken_indices_result.taken_indices.Contains(order_index))
                    order_index++;

                /* Adjust script execution order */
                MonoImporter.SetExecutionOrder(monoscript, order_index);
                ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Script [" + monoscript.name + "] exec order changed to " + order_index);
                taken_indices_result.taken_indices.Add(order_index);
            }
            
            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Finished script execution order changes!");
            ArborLog("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Script Execution Order should now be consistent across editor and builds. Contact ayarger@umich.edu with any issues.");
        }
        catch (Exception e)
        {
            Debug.LogError("[IT487ExecutionOrderSync.SyncronizeExecutionOrderBetweenEditorAndBuilds] Something went wrong. Contact ayarger@umich.edu. Here's the message [" + e.Message + "] and stacktrace [" + e.StackTrace + "]");
            EditorPrefs.SetInt("syncing_execution_order_operations_remaining", 0);
        }

        EditorApplication.UnlockReloadAssemblies();
    }

    /* Obtain all "currently-taken" script execution order indices...
     * (1) monoscripts that have been assigned an explicit index via the Unity Editor.
     * (2) monoscripts that have been assigned an explicit index via the DefaultExecutionOrder attribute.
     */
    static ScriptExecutionTakenIndexResult GetUnavailableScriptExecutionIndices(List<MonoScript> mono_scripts)
    {
        ScriptExecutionTakenIndexResult result = new ScriptExecutionTakenIndexResult();

        foreach (MonoScript monoscript in mono_scripts)
        {
            /* Include Attribute order index */
            System.Type monobehavior_type = monoscript.GetClass();
            Attribute[] attrs = Attribute.GetCustomAttributes(monobehavior_type);
            foreach (Attribute attr in attrs)
            {
                DefaultExecutionOrder deo = attr as DefaultExecutionOrder; // DIIIIOOOOOOOOO!

                if (deo != null)
                {
                    if (deo.order != 0)
                    {
                        result.taken_indices.Add(deo.order);
                        result.customized_monoscripts.Add(monoscript);
                    }
                    break;
                }
            }

            /* Include editor order index (are these two the same?) */
            int current_order = MonoImporter.GetExecutionOrder(monoscript);
            if (current_order != 0)
            {
                result.taken_indices.Add(current_order);
                result.customized_monoscripts.Add(monoscript);
            }
        }

        if (result.taken_indices.Contains(0))
            result.taken_indices.Remove(0);

        return result;
    }
}

public class ScriptExecutionTakenIndexResult
{
    public HashSet<int> taken_indices = new HashSet<int>();
    public HashSet<MonoScript> customized_monoscripts = new HashSet<MonoScript>();
}