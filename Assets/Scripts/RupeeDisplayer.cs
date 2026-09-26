using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RupeeDisplayer : MonoBehaviour
{

    public Inventory inventory;
    TMP_Text rupeeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rupeeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && rupeeText != null)
        {
            rupeeText.text = "Rupees: " + inventory.GetRupees().ToString();
        }
    }
}
