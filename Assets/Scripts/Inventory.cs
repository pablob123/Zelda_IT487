using UnityEngine;

public class Inventory : MonoBehaviour
{

    int rupee_count = 0;
    public int health_count = 6;
    public int bomb_count = 8;
    public int key_count = 0;

    public static bool god_mode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && god_mode == false)
        {
            god_mode = true;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && god_mode == true)
        {
            god_mode = false;
        }

        if (god_mode == true)
        {
            health_count = 6;
            bomb_count = 8;
            rupee_count = 999;
        }
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
        if(health_count < 6)
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

    public void ChangeKey(int num_keys)
    {
        if(key_count > 0)
        {
            key_count += num_keys;
        }
        if (key_count == 0 && num_keys > 0)
        {
            key_count += num_keys;
        }
    }
    public int GetBombs()
    {
        return bomb_count;
    }

    public int GetKey()
    {
        return key_count;
    }

    public bool GetGodMode()
    {
        return god_mode;
    }
}
