using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public Weapon weapon;

    [Header("Stats")]
    public int ammoToAdd;

    private void OnTriggerEnter(Collider other) {
        weapon.ammo += ammoToAdd;
        Destroy(gameObject);
    }
}
