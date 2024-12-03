using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> weaponList;
    private int currentWeapon;


    private void Update() {
        string keyInput = Input.inputString;
        switch(keyInput) {
            case "1":
                SwitchWeapon(0);
                break;
            case "2":
                SwitchWeapon(1);
                break;
            case "3":
                SwitchWeapon(2);
                break;
            case "4":
                SwitchWeapon(3);
                break;
            case "5":
                SwitchWeapon(4);
                break;
        }
    }

    private void SwitchWeapon(int weaponToSwitch) {
        weaponList[weaponToSwitch].SetActive(true);
        weaponList[currentWeapon].SetActive(false);
        currentWeapon = weaponToSwitch;
    }
}
