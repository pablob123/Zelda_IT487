using UnityEngine;

public class Inventory : MonoBehaviour
{

    int rupee_count = 0;
    public int health_count = 16;
    public int bomb_count = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
    }
    public int GetRupees()
    {
        return rupee_count;
    }

    public void AddHealth(int num_health)
    {
        if(health_count < 16)
        {
            health_count += num_health;
        }
    }
    public int GetHealth()
    {
        return health_count;
    }

    public void AddBombs(int num_bombs)
    {
        if (bomb_count < 8)
        {
            bomb_count += num_bombs;
        }
    }
    public int GetBombs()
    {
        return bomb_count;
    }
}
