using UnityEngine;
using System.Collections;

public class frozenByBoomerang : MonoBehaviour
{
    public Component movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent(movement.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void freeze()
    {
        StartCoroutine(frozen(movement));
    }

    IEnumerator frozen(Component movement)
    {
        if(movement is Behaviour behaviour)
        {
            behaviour.enabled = false;
        }

        yield return null;
    }
}
