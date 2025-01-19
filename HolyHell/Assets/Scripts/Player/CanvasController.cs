using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public PlayerSystem playerSystem;
    public List<GameObject> weaponList;

    [Header("Canvas Elements")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI miniStatusText;
    public Slider healthSlider;
    public Slider healthBonusSlider;
    public Slider armorSlider;
    public Slider armorBonusSlider;
    public GameObject redKey;
    public GameObject blueKey;
    public GameObject yellowKey;

    private GameObject activeWeapon;
    private float statusTimer, miniStatusTimer;
    private bool statusOn, miniStatusOn;

    private void Awake() {
        playerSystem = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update() {
        SetPlayerHealth();
        SetPlayerArmor();

        FindCurrentActiveWeapon();
        if (activeWeapon != null)
        {
            SetAmmoText();
        }

        if(statusOn) statusTimer += Time.deltaTime;
        if(statusTimer > 3.0f) {
            statusText.text =  "";
            statusOn = false;
        }

        if(miniStatusOn) miniStatusTimer += Time.deltaTime;
        if(miniStatusTimer > 3.0f) {
            miniStatusText.text =  "";
            miniStatusOn = false;
        }
    }

    public void SetStatusText(string text) {
        statusText.text = text;
        statusTimer = 0.0f;
        statusOn = true;
    }

    public void SetMiniStatusText(string text) {
        miniStatusText.text = text;
        miniStatusTimer = 0.0f;
        miniStatusOn = true;
    }

    private void ResetStatusText() {
        statusText.text = "";
    }

    private void SetAmmoText() {
        Weapon weapon = activeWeapon.GetComponent<Weapon>();
        if(weapon.startAmmo == -1) {
            ammoText.text = "- | ∞";
        } else {
            ammoText.text = weapon.ammo + " | " + weapon.maxAmmo;
        }
        weaponNameText.text = weapon.name;
    }

    private void SetPlayerHealth() {
        if(playerSystem.health <= healthSlider.maxValue) {
            healthSlider.value = playerSystem.health;
            healthBonusSlider.value = healthSlider.minValue;
        } else {
            healthSlider.value = healthSlider.maxValue;
            healthBonusSlider.value = playerSystem.health - healthBonusSlider.maxValue;
        }
        healthText.text = playerSystem.health + " | 200";
    }

    private void SetPlayerArmor() {
        if(playerSystem.armor <= armorSlider.maxValue) {
            armorSlider.value = playerSystem.armor;
            armorBonusSlider.value = armorSlider.minValue;
        } else {
            armorSlider.value = armorSlider.maxValue;
            armorBonusSlider.value = playerSystem.armor - armorBonusSlider.maxValue;
        }
        armorText.text = playerSystem.armor + " | 200";
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

    public void RestartLevel() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
