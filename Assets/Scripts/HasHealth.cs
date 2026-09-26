using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HasHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
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
        Debug.Log("You Died!");

    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
