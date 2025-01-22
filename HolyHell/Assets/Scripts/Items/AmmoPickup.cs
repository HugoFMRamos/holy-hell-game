using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickUp
{
    [Header("Specific Stats")]
    public Weapon weapon;
    public Inventory inventory;
    public string weaponName;

    private void Start() {
        GameManager.OnPlayerInstantiated += HandlePlayerInstantiated;

        // Check if the player is already cached
        if (GameManager.CachedPlayerInstance != null)
        {
            HandlePlayerInstantiated(GameManager.CachedPlayerInstance);
        }
    }

    private void HandlePlayerInstantiated(GameObject playerInstance)
    {
        playerObject = playerInstance;
        inventory = playerObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();
        playerHUD = playerObject.transform.GetChild(1).GetComponent<CanvasController>();

        foreach(GameObject gameObject in inventory.weaponList) {
            if(gameObject.name == weaponName) {
                weapon = gameObject.GetComponent<Weapon>();
            }
        }
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
