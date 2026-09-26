using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{

    public HasHealth health;
    TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health != null && healthText != null)
        {
            healthText.text = "Health: " + health.GetCurrentHealth().ToString();
        }
    }
}
