using UnityEngine;
using System.Collections;

public class AquamentusMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    GameObject Fireball;

    private GameObject fireBall1;
    private GameObject fireBall2;
    private GameObject fireBall3;

    float randomPositionX;
    float randomFire;
    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            randomPositionX = Random.Range(-3f, 4f);
            randomFire = Random.Range(2, 5);
            StartCoroutine(aquamentusMove());
        }
    }

    IEnumerator aquamentusMove()
    {
        bool hasFired = false;
        float elapsedTime = 0.0f;
        Vector3 newPosition = new Vector3(this.transform.position.x + randomPositionX, this.transform.position.y, this.transform.position.z);
        while (Vector3.Distance(rb.position, newPosition) > 0.1f)
        {
            this.transform.position = Vector3.MoveTowards(rb.position, newPosition, 1f * Time.deltaTime);

            if(elapsedTime >= randomFire &&  hasFired == false)
            {
                hasFired = true;
                Vector3 fireballPos1 = new Vector3(this.transform.position.x, this.transform.position.y+0.9f, this.transform.position.z);
                Vector3 fireballPos2 = new Vector3(this.transform.position.x, this.transform.position.y+0.6f, this.transform.position.z);
                Vector3 fireballPos3 = new Vector3(this.transform.position.x, this.transform.position.y+0.3f, this.transform.position.z);
                fireBall1 = Object.Instantiate(Fireball, fireballPos1, Quaternion.identity);
                fireBall2 = Object.Instantiate(Fireball, fireballPos2, Quaternion.identity);
                fireBall3 = Object.Instantiate(Fireball, fireballPos3, Quaternion.identity);
                fireBall1.SetActive(true);
                fireBall2.SetActive(true); 
                fireBall3.SetActive(true);
                fireBall1.GetComponent<fireballMovement>().fireBallStart(fireBall1, -10f, 5f);
                fireBall2.GetComponent<fireballMovement>().fireBallStart(fireBall2, -10f, 0f);
                fireBall3.GetComponent<fireballMovement>().fireBallStart(fireBall3, -10f, -5f);
                randomFire = Random.Range(2, 5);
                elapsedTime = 0;
            }

            hasFired = false;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            Debug.Log("E");
            randomPositionX = Random.Range(-4f, -1f);
        }
    }
}
