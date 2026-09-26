using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HasHealth : MonoBehaviour
{
    Inventory inventory;
    public int maxHealth = 6;
    private int currentHealth;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(inventory != null)
        {
            Debug.Log("You Died!");
        }

    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (inventory != null && inventory.GetGodMode())
        {
            currentHealth = 6;
        }
    }
}
