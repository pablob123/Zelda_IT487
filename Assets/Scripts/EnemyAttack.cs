using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Vector2 knockbackDirection =
                    collision.transform.position - transform.position;

                playerHealth.TakeDamage(damage, knockbackDirection);
            }
        }
    }

    private class PlayerHealth
    {
        internal void TakeDamage(int damage, Vector2 knockbackDirection)
        {
            throw new NotImplementedException();
        }
    }
}