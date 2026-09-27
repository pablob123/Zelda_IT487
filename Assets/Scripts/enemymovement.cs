using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform player;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}