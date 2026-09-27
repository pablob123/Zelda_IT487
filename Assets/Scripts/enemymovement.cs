using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform player;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}