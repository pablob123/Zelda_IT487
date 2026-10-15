using UnityEngine;
using System.Collections;

public class fireballMovement : MonoBehaviour
{
    
    private GameObject fireBall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void fireBallStart(GameObject fireball, float moveX, float moveY)
    {
        fireBall = fireball;
        StartCoroutine(fireMove(fireBall, moveX, moveY));
        
    }

    public IEnumerator fireMove(GameObject fireball, float moveX, float moveY)
    {
        
        Vector3 newPosition = new Vector3(moveX, moveY).normalized;

        while(Vector3.Distance(fireball.transform.position, newPosition) > 0.1f){
            fireball.transform.position += newPosition * 5f * Time.deltaTime;
            if (Physics.Raycast(fireball.transform.position, newPosition, out RaycastHit hit, .5f))
            {
                if (hit.collider.CompareTag("wall"))
                {

                    Destroy(fireball);
                    break;
                }
            }

            yield return null;
        }
        yield return null;
    }

}
