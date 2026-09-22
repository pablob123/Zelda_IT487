using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{

    public Inventory inventory;
    TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null && healthText != null)
        {
            healthText.text = inventory.GetHealth().ToString();
        }
    }
}
