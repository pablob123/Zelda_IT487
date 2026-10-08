using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class SpikeTrapMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool isMoving = false;
    private float attackTime;
    private float returnTime;

    private float positionX;
    private float positionY;
    private float newX;
    private float newY;

    [SerializeField]
    bool moveUpToggle;
    [SerializeField]
    bool moveDownToggle;
    [SerializeField]
    bool moveRightToggle;
    [SerializeField]
    bool moveLeftToggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        newX = positionX;
        newY = positionY;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit hitUp, 6f) && moveUpToggle)
            {
                if (hitUp.collider.gameObject.CompareTag("Player"))
                {
                    isMoving = true;
                    newY = positionY + 3f;
                    newX = positionX;
                    attackTime = 0.5f;
                    returnTime = 2.0f;
                    StartCoroutine(spikeTrapAttack(newX, newY, attackTime));
                }
            }
            if (Physics.Raycast(this.transform.position, -Vector3.up, out RaycastHit hitDown, 6f) && moveDownToggle)
            {
                if (hitDown.collider.gameObject.CompareTag("Player"))
                {
                    isMoving = true;
                    newY = positionY - 3f;
                    newX = positionX;
                    attackTime = 0.5f;
                    returnTime = 2.0f;
                    StartCoroutine(spikeTrapAttack(newX, newY, attackTime));
                }
            }
            if (Physics.Raycast(this.transform.position, Vector3.right, out RaycastHit hitRight, 11f) && moveRightToggle)
            {
                if (hitRight.collider.gameObject.CompareTag("Player"))
                {
                    isMoving = true;
                    newY = positionY;
                    newX = positionX + 6f;
                    attackTime = 1.0f;
                    returnTime = 3.0f;
                    StartCoroutine(spikeTrapAttack(newX, newY, attackTime));
                }
            }

            if (Physics.Raycast(this.transform.position, -Vector3.right, out RaycastHit hitLeft, 11f) && moveLeftToggle)
            {
                if (hitLeft.collider.gameObject.CompareTag("Player"))
                {
                    isMoving = true;
                    newY = positionY;
                    newX = positionX - 6f;
                    attackTime = 1.0f;
                    returnTime = 3.0f;
                    StartCoroutine(spikeTrapAttack(newX, newY, attackTime));
                }
            }
        }
    }

    IEnumerator spikeTrapAttack(float moveX, float moveY, float time)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(newX, newY, 0f), 5f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(spikeTrapReturn(returnTime));
        yield return null;
    }

    IEnumerator spikeTrapReturn(float time)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(positionX, positionY, 0f), 2f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
        yield return null;
    }
}
