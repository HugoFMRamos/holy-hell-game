using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public PlayerSystem playerSystem;
    public List<GameObject> weaponList;

    [Header("Canvas Elements")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI statusText;
    public GameObject redKey;
    public GameObject blueKey;
    public GameObject yellowKey;

    private GameObject activeWeapon;

    private void Awake() {
        playerSystem = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        SetPlayerHealth();
        SetPlayerArmor();
        SetSpeedText();

        FindCurrentActiveWeapon();
        if (activeWeapon != null)
        {
            SetAmmoText();
        }


    }

    public void SetStatusText(string text) {
        statusText.text = text;
        Invoke(nameof(ResetStatusText), 3f);
    }

    private void ResetStatusText() {
        statusText.text = "";
    }

    private void SetSpeedText() {
        speedText.text = "Speed: " + playerController.currentSpeed;
    }

    private void SetAmmoText() {
        Weapon weapon = activeWeapon.GetComponent<Weapon>();
        if(weapon.startAmmo == -1) {
            ammoText.text = weapon.name + ": Inf";
        } else {
            ammoText.text = weapon.name + ": " + weapon.ammo;
        }
    }

    private void SetPlayerHealth() {
        healthText.text = "Health: " + playerSystem.health;
    }

    private void SetPlayerArmor() {
        armorText.text = "Armor: " + playerSystem.armor;
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

    public void EnableKey(int keyToEnable) {
        switch(keyToEnable) {
            case 0:
                redKey.SetActive(true);
                break;
            case 1:
                blueKey.SetActive(true);
                break;
            case 2:
                yellowKey.SetActive(true);
                break;
        }
    }
}
