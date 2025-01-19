using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickUp
{
    [Header("Specific Stats")]
    public Weapon weapon;

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
