using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] private int currentWeapon;
    [SerializeField] private List<int> weaponAmmoList;
    [SerializeField] private List<string> weaponsInInventory;

    // Constructor
    public PlayerData(int health, int armor, int currentWeapon, List<int> weaponAmmoList, List<string> weaponsInInventory)
    {
        this.health = health;
        this.armor = armor;
        this.currentWeapon = currentWeapon;
        this.weaponAmmoList = weaponAmmoList;
        this.weaponsInInventory = weaponsInInventory;
    }

    // Getters and Setters
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public int CurrentWeapon
    {
        get { return currentWeapon; }
        set { currentWeapon = value; }
    }

    public List<int> WeaponAmmoList
    {
        get { return weaponAmmoList; }
        set { weaponAmmoList = value; }
    }

    public List<string> WeaponsInInventory
    {
        get { return weaponsInInventory; }
        set { weaponsInInventory = value; }
    }


}