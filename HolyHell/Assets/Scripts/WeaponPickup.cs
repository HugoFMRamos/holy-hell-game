using UnityEngine;

public class WeaponPickup : PickUp {
    
    [Header("Stats")]
    public Weapon weapon;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if (!weapon.isInInventory) {
            weapon.isInInventory = true;
        }

        else if (weapon.ammo < weapon.maxAmmo) {
            weapon.ammo += base.valueToAdd;
            weapon.ammo = Mathf.Min(weapon.ammo, weapon.maxAmmo);
        }

        Destroy(gameObject);
    }
}