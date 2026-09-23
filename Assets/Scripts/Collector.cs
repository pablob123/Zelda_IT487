using UnityEngine;

public class Collector : MonoBehaviour
{

    Inventory inventory;
    public AudioClip Rupee_collection_sound_clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();

        if(inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject object_collided_with = other.gameObject;

        if (object_collided_with.CompareTag("rupee"))
        {
            inventory.AddRupees(1);
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(Rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.CompareTag("heart"))
        {
            inventory.AddHealth(1);
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(Rupee_collection_sound_clip, Camera.main.transform.position);
        }
        if (object_collided_with.CompareTag("bomb"))
        {
            inventory.AddBombs(1);
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(Rupee_collection_sound_clip, Camera.main.transform.position);
        }
    }
}
