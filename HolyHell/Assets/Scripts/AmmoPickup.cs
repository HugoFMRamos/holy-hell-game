using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickUp
{
    [Header("Specific Stats")]
    public Weapon weapon;

    private void OnTriggerEnter(Collider other) {
        weapon.ammo += base.valueToAdd;
        Destroy(gameObject);
    }
}
