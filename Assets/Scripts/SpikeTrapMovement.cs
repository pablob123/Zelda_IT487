using UnityEngine;
using System.Collections;

public class SpikeTrapMovement : MonoBehaviour
{
    private bool isMoving = false;
    private float moveSpeed = 3;

    private float positionX;
    private float positionY;
    private float newX;
    private float newY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        newX = positionX;
        newY = positionY;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isMoving == false)
        {
            isMoving = true;
            StartCoroutine(spikeTrapIdle());
        }

    }


    IEnumerator spikeTrapIdle()
    {
        Debug.DrawRay(this.transform.position, this.transform.up * 11f, Color.red, 5f);
        Debug.DrawRay(this.transform.position, -this.transform.up * 11f, Color.red, 5f);
        Debug.DrawRay(this.transform.position, this.transform.right * 6f, Color.red, 5f);
        Debug.DrawRay(this.transform.position, -this.transform.right * 6f, Color.red, 5f);
        if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit hitUp, 11f) &&
           Physics.Raycast(this.transform.position, -Vector3.up, out RaycastHit hitDown, 11f) &&
           Physics.Raycast(this.transform.position, Vector3.right, out RaycastHit hitRight, 6f) &&
           Physics.Raycast(this.transform.position, -Vector3.right, out RaycastHit hitLeft, 6f))
        {
            if (hitUp.collider.CompareTag("Player"))
            {
                newY = positionY + 3f;
            }
            else if (hitDown.collider.CompareTag("Player"))
            {
                newY = positionY - 3f;
            }
            else if (hitRight.collider.CompareTag("Player"))
            {
                newX = positionX + 5.5f;
            }
            else if (hitLeft.collider.CompareTag("Player"))
            {
                newX = positionX - 5.5f;
            }

            StartCoroutine(spikeTrapAttack(newX, newY));
            yield return null;
        }

        yield return null;
    }

    IEnumerator spikeTrapAttack(float moveX, float moveY)
    {


        yield return null;
    }

    IEnumerator spikeTrapReturn()
    {
        yield return null;
    }
}
