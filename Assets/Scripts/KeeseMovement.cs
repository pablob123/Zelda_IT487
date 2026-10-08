using UnityEngine;
using System.Collections;
using static UnityEngine.UI.ScrollRect;

public class KeeseMovement : MonoBehaviour
{
    private int moveTime;
    private int stopTime = 2;
    private bool isMoving = false;
    private Rigidbody rb;

    private float randomX;
    private float randomY;
    private float newX;
    private float newY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == false)
        {
            moveTime = Random.Range(3, 21);
            isMoving = true;
            StartCoroutine(keeseMove(moveTime));
        }
    }

    IEnumerator keeseMove(int movementTime)
    {
        float elapsedTime = 0.0f;

        randomDirection();

        newX = this.transform.position.x + randomX;
        newY = this.transform.position.y + randomY;

        while (elapsedTime < movementTime)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(newX, newY, 0f), 2 * Time.deltaTime);

            if(rb.position.x == newX && rb.position.y == newY)
            {
                randomDirection();
                newX = this.transform.position.x + randomX;
                newY = this.transform.position.y + randomY;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(keeseRest());
        yield return null;

    }
    IEnumerator keeseRest()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < stopTime)
        {

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //moveTime = Random.Range(3, 21);
        //StartCoroutine(keeseMove(moveTime));
        yield return null;
        isMoving = false;
        
    }

    private void randomDirection()
    {
         randomX = Random.Range(-5.5f, 5.5f);
         randomY = Random.Range(-3f, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            randomDirection();

            newX = -(newX) + randomX;
            newY = -(newY) + randomY;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(newX, newY, 0f), 2 * Time.deltaTime);
        }
    }
}
