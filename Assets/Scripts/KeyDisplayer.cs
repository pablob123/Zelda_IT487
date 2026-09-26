using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplayer : MonoBehaviour
{

    public Inventory inventory;
    TMP_Text keyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null && keyText != null)
        {
            keyText.text = "Keys: " + inventory.GetKey().ToString();
        }
    }
}
