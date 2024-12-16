using System;
using UnityEngine;

public class PlayerSystem : MonoBehaviour {
    
    [Header("Player Stats")]
    public int health = 100;
    public int maxHealth = 200;
    public int armor = 100;
    public int maxArmor = 200;
    public float armorAbsorbtionPercentage = .8f;
    public bool hasPlayerDied;
    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject playerCamera;

    [Header("Player Keys")]
    public bool hasRedKey;
    public bool hasBlueKey;
    public bool hasYellowKey;



    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            DamageMe(20);
        }

        if(health <= 0 && !hasPlayerDied) {
            hasPlayerDied = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        player.SetActive(false);
        playerCamera.GetComponent<CameraController>().enabled = false;
        gameOverScreen.SetActive(true);
    }

    public void DamageMe(int damage) {
        if (armor == 0) {
            health -= damage;
            if (health < 0) {
                health = 0;
            }
            return;
        }

        int damageToArmor = (int)(damage * armorAbsorbtionPercentage);
        int damageToHealth = damage - damageToArmor;

        armor -= damageToArmor;
        if (armor < 0) {
            armor = 0;
        }

        health -= damageToHealth;
        if (health < 0) {
            health = 0;
        }
    }

    public void HealMe(int valueToAdd, bool addToHealth) {
        if(addToHealth) {
            health += valueToAdd;
            if(health > maxHealth) {
                health = maxHealth;
            }
        }
        else {
            armor += valueToAdd;
            if(armor > maxArmor) {
                armor = maxArmor;
            }
        }
    }
}