using UnityEngine;

public class DropKey : MonoBehaviour
{
    [SerializeField]
    private GameObject Key;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnKey()
    {
        Key = Object.Instantiate(Key, this.transform.position, Quaternion.identity);
    }
}
