using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    int rupee_count = 0;
    public int health_count = 6;
    public int bomb_count = 8;
    public int key_count = 0;
    public bool hasBow = false;
    public bool hasBoomerang = false;
    public bool hasBomb = false;


    public List<int> weapons = new List<int>();
    public int weaponsNumCount = -1;
    public int currentWeapon = 0;
    private int setWeapon = -1;

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
            key_count = 999;
            AddBow();
            AddBoomerang();
            AddBombs(999);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(weapons.Count != 0)
            {
                setWeapon = sortWeapons();  
            }
              
        }

        if (setWeapon == 1)
        {
            hasBow = true;
            hasBoomerang = false;
            hasBomb = false;
        }
        else if (setWeapon == 2)
        {
            hasBow = false;
            hasBoomerang = true;
            hasBomb = false;
        }
        else if (setWeapon == 3)
        {
            hasBow = false;
            hasBoomerang = false;
            hasBomb = true;
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
        if (!weapons.Contains(3))
        {
            weaponsNumCount++;
            setWeapon = 3;
            weapons.Insert(weaponsNumCount, 3);
        }

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
    public void AddBow()
    {
        weaponsNumCount++;
        hasBow = true;
        setWeapon = 1;
        weapons.Insert(weaponsNumCount, 1);

    }
    public bool GetBow()
    {
        return hasBow;
    }
    public void AddBoomerang()
    {
        weaponsNumCount++;
        hasBoomerang = true;
        setWeapon = 2;
        weapons.Insert(weaponsNumCount, 2);
    }
    public bool GetBoomerang()
    {
        return hasBoomerang;
    }

    public string GetWeapon()
    {
        if (setWeapon == 1)
        {
            return "Bow";
        }
        else if(setWeapon == 2)
        {
            return "Boomerang";
        }
        else if(setWeapon == 3)
        {
            return "Bomb";
        }
        return null;
    }

    public bool GetGodMode()
    {
        return god_mode;
    }

    public int sortWeapons()
    {
        if (weapons.Count == 0)
        {
            return -1;
        }
        else
        {
            if(currentWeapon < weaponsNumCount)
            {
                currentWeapon++;
            }
            else if(currentWeapon == weaponsNumCount)
            {
                currentWeapon = 0;
            }
        }

        return weapons[currentWeapon];
    }
}
