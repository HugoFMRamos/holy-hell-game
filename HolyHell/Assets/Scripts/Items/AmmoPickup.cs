using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickUp
{
    [Header("Specific Stats")]
    public Weapon weapon;
    public Inventory inventory;
    public string weaponName;

    private void Awake() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        foreach(GameObject gameObject in inventory.weaponList) {
            if(gameObject.name == weaponName) {
                weapon = gameObject.GetComponent<Weapon>();
            }
        }

        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if (weapon.ammo < weapon.maxAmmo) {
            weapon.ammo += base.valueToAdd;
            weapon.ammo = Mathf.Min(weapon.ammo, weapon.maxAmmo);
        }

        string text = weapon.name + "ammo! (+" + base.valueToAdd + ")";
        playerHUD.SetMiniStatusText(text);
        Destroy(gameObject);
    }
}
