using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    HasHealth health;
    public int damage = 1;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.GetComponent<HasHealth>().TakeDamage(damage);

            //if (playerHealth != null)
            //{
            //    Vector2 knockbackDirection =
            //        collision.transform.position - transform.position;

            //    playerHealth.TakeDamage(damage, knockbackDirection);
            //}
        }
    }

    private class PlayerHealth
    {
        internal void TakeDamage(int damage, Vector2 knockbackDirection)
        {

            //throw new NotImplementedException();
        }
    }
}