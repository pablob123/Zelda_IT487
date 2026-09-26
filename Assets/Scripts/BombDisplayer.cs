using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombDisplayer : MonoBehaviour
{

    public Inventory inventory;
    TMP_Text bombText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bombText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && bombText != null)
        {
            bombText.text = "Bombs: " + inventory.GetBombs().ToString();
        }
    }
}
