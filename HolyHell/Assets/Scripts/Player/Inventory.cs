using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<GameObject> weaponList;
    public List<GameObject> animatorObjects;
    public KeyCode waferKey;
    public KeyCode shotgunKey;
    public KeyCode grenadeKey;
    public KeyCode scepterKey;
    public int currentWeapon = 3;

    private void Update() {
        if(Input.GetKeyDown(waferKey)) {
            SwitchWeapon(0);
        } else if (Input.GetKeyDown(shotgunKey)) {
            SwitchWeapon(1);
        } else if (Input.GetKeyDown(grenadeKey)) {
            SwitchWeapon(2);
        } else if (Input.GetKeyDown(scepterKey)) {
            SwitchWeapon(3);
        }
    }

    public void SwitchWeapon(int weaponToSwitch) {
        Weapon weapon = weaponList[weaponToSwitch].GetComponent<Weapon>();

        if(weaponToSwitch == currentWeapon) {
            return;
        }
        else if(weapon.isInInventory) {
            weaponList[weaponToSwitch].SetActive(true);
            animatorObjects[weaponToSwitch].SetActive(true);
            weaponList[currentWeapon].SetActive(false);
            animatorObjects[currentWeapon].SetActive(false);
            currentWeapon = weaponToSwitch;
        }
    }
}
