using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public List<GameObject> weaponList;

    [Header("Canvas Elements")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI ammoText;

    private GameObject activeWeapon;

    private void Update() {
        SetSpeedText();

        FindCurrentActiveWeapon();
        if (activeWeapon != null)
        {
            Weapon weapon = activeWeapon.GetComponent<Weapon>();
            ammoText.text = "Ammo: " + weapon.ammo;
        }
    }

    private void SetSpeedText() {
        speedText.text = "Speed: " + playerController.currentSpeed;
    }

    private void SetAmmoText() {
        ammoText.text = "Ammo: " + activeWeapon.GetComponent<Weapon>().ammo;
    }

    private void FindCurrentActiveWeapon() {
        if(activeWeapon != null && activeWeapon.activeSelf) {
            return;
        }

        foreach(GameObject weapon in weaponList) {
            if(weapon.activeSelf) {
                activeWeapon = weapon;
                break;
            }
        }
    }
}
