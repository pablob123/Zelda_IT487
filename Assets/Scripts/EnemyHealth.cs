using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 6;
    private int currentHealth;

    [Header("Knockback")]
    public float knockbackForce = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        currentHealth -= damage;

        Debug.Log("Enemy Health: " + currentHealth);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(
                hitDirection.normalized * knockbackForce,
                ForceMode2D.Impulse
            );
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");

        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}