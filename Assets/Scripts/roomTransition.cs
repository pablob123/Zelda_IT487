using UnityEngine;
using System.Collections;

public class roomTransition : MonoBehaviour
{
    Inventory inventory;

    private Vector3 cameraPosition = Vector3.zero;
    Vector3 playerPosition = Vector3.zero;
    public Camera camera;
    private float durationTime = 2.0f;

    private bool changingRoom = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
        cameraPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = this.transform.position;

        //Left
        if(Camera.main.WorldToViewportPoint(playerPosition).x < 0 && changingRoom == false)
        {
            StartCoroutine(doorLeft());
            changingRoom = true;
        }
        //Right
        if (Camera.main.WorldToViewportPoint(playerPosition).x > 1 && changingRoom == false)
        {
            StartCoroutine(doorRight());
            changingRoom = true;
        }
        //Down
        if (Camera.main.WorldToViewportPoint(playerPosition).y < 0 && changingRoom == false)
        {
            StartCoroutine(doorDown());
            changingRoom = true;
        }
        //Up
        if (Camera.main.WorldToViewportPoint(playerPosition).y > .7 && changingRoom == false)
        {
            StartCoroutine(doorUp());
            changingRoom = true;
        }
    }

    IEnumerator doorDown()
    {
        float elapsedTime = 0.0f;
        float newCameraPosition = cameraPosition.y - 11f;
        float newPlayerPosition = playerPosition.y - 2f;

        while (elapsedTime < durationTime)
        {
            camera.transform.position = Vector3.Lerp(cameraPosition, new Vector3(cameraPosition.x, newCameraPosition, cameraPosition.z), elapsedTime / durationTime);
            this.transform.position = Vector3.Lerp(playerPosition, new Vector3(playerPosition.x, newPlayerPosition, playerPosition.z), elapsedTime / (durationTime * 2.0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraPosition = camera.transform.position;
        changingRoom = false;
    }
    IEnumerator doorUp()
    {
        float elapsedTime = 0.0f;
        float newCameraPosition = cameraPosition.y + 11f;
        float newPlayerPosition = playerPosition.y + 2f;

        while (elapsedTime < durationTime)
        {
            camera.transform.position = Vector3.Lerp(cameraPosition, new Vector3(cameraPosition.x, newCameraPosition, cameraPosition.z), elapsedTime / durationTime);
            this.transform.position = Vector3.Lerp(playerPosition, new Vector3(playerPosition.x, newPlayerPosition, playerPosition.z), elapsedTime / (durationTime * 2.0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraPosition = camera.transform.position;
        changingRoom = false;
    }
    IEnumerator doorLeft()
    {
        float elapsedTime = 0.0f;
        float newCameraPosition = cameraPosition.x - 16f;
        float newPlayerPosition = playerPosition.x - 2f;

        while (elapsedTime < durationTime)
        {
            camera.transform.position = Vector3.Lerp(cameraPosition, new Vector3(newCameraPosition, cameraPosition.y, cameraPosition.z), elapsedTime / durationTime);
            this.transform.position = Vector3.Lerp(playerPosition, new Vector3(newPlayerPosition, playerPosition.y, playerPosition.z), elapsedTime / (durationTime * 2.0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraPosition = camera.transform.position;
        changingRoom = false;
    }
    IEnumerator doorRight()
    {
        float elapsedTime = 0.0f;
        float newCameraPosition = cameraPosition.x + 16f;
        float newPlayerPosition = playerPosition.x + 2f;

        while (elapsedTime < durationTime)
        {
            camera.transform.position = Vector3.Lerp(cameraPosition, new Vector3(newCameraPosition, cameraPosition.y, cameraPosition.z), elapsedTime / durationTime);
            this.transform.position = Vector3.Lerp(playerPosition, new Vector3(newPlayerPosition, playerPosition.y, playerPosition.z), elapsedTime / (durationTime * 2.0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraPosition = camera.transform.position;
        changingRoom = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("doorLock"))
        {
            if(inventory.GetKey() >= 1)
            {
                Destroy(collision.gameObject);
                inventory.ChangeKey(-1);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("doorleft"))
    //    {
    //        Debug.Log("E");
    //    }
    //}
}
