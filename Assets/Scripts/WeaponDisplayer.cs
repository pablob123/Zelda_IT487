using TMPro;
using UnityEngine;

public class WeaponDisplayer : MonoBehaviour
{
    public Inventory inventory;
    TMP_Text weaponText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null && weaponText != null)
        {
            weaponText.text = "Current Weapon: " + inventory.GetWeapon();
        }
        
    }
}
