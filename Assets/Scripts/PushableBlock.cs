using UnityEngine;
using System.Collections;

public class PushableBlock : MonoBehaviour
{
    private float pushTime = 1.0f;
    private bool canPush = false;
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && canPush == false)
        {
            StartCoroutine(moveBlock(collision));
        }

    }

    IEnumerator moveBlock(Collision collision)
    {
        yield return new WaitForSeconds(pushTime);
        if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkUp"))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 1, 0), 3);
        }
        if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkDown"))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - 1, 0), 3);
        }
        if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkRight"))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.x + 1, 0), 3);
        }
        if (collision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WalkLeft"))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.x - 1, 0), 3);
        }
        canPush = false;
        yield return null;
    }
}
