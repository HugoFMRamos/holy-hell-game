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
    public KeyCode bibleKey;
    private int currentWeapon = 4;
    
    private IEnumerator Start() {
        yield return new WaitForSeconds(.001f);
        ClearInventory(); 
    }

    private void Update() {
        if(Input.GetKeyDown(waferKey)) {
            SwitchWeapon(0);
        } else if (Input.GetKeyDown(shotgunKey)) {
            SwitchWeapon(1);
        } else if (Input.GetKeyDown(grenadeKey)) {
            SwitchWeapon(2);
        } else if (Input.GetKeyDown(scepterKey)) {
            SwitchWeapon(3);
        } else if (Input.GetKeyDown(bibleKey)) {
            SwitchWeapon(4);
        }
    }

    private void SwitchWeapon(int weaponToSwitch) {
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

    private void ClearInventory() {
        for(int i = 0; i < weaponList.Count; i++) {
            weaponList[i].SetActive(false);
            animatorObjects[i].SetActive(false);
        }
        SwitchWeapon(0);
    }
}
