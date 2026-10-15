using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private GameObject heart;
    [SerializeField]
    private GameObject rupee;
    [SerializeField]
    private GameObject bomb;
    public HasHealth playerHealth;
    public Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropRandomItem()
    {
        int pickDrop;
        if (playerHealth != null)
        {
            if(playerHealth.GetCurrentHealth() <= 3)
            {
                pickDrop = Random.Range(1, 11);
            }
            else
            {
                pickDrop = Random.Range(1, 13);
            }
            if (pickDrop >= 1 && pickDrop <= 4)
            {
                return;
            }
            else if(pickDrop >= 5 &&  pickDrop <= 6)
            {
                rupee = Object.Instantiate(rupee, this.transform.position, Quaternion.identity);
            }
            else if(pickDrop  <= 7 && pickDrop <= 8)
            {
                bomb = Object.Instantiate(bomb, this.transform.position, Quaternion.identity);
            }
            else if(pickDrop < 9 && pickDrop <= 12)
            {
                heart = Object.Instantiate(heart, this.transform.position, Quaternion.identity);
            }
        }
        
    }
}
