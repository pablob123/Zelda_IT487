using UnityEngine;
using System.Collections;
using System;

public class GelMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    private Vector2 moveDirection;
    private bool isActive = false;
    private Rigidbody rb;
    private int dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        // TEMP: activate immediately for testing
        ActivateGel();
    }

    void FixedUpdate()
    {
        if (!isActive) return;
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void ActivateGel()
    {
        isActive = true;
        switch (dir)
        {
            case 0: moveDirection = Vector2.up; break;
            case 1: moveDirection = Vector2.down; break;
            case 2: moveDirection = Vector2.left; break;
            case 3: moveDirection = Vector2.right; break;
        }
    }

    private void Next(int v1, int v2)
    {
        throw new NotImplementedException();
    }
}