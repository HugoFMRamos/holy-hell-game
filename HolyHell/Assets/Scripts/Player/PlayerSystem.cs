using System;
using System.Threading;
using UnityEngine;

public class PlayerSystem : MonoBehaviour {
    
    [Header("Player Stats")]
    public int health = 100;
    public int maxHealth = 200;
    public int armor = 100;
    public int maxArmor = 200;
    public int enemiesAggroed = 0;
    public float armorAbsorbtionPercentage = .8f;
    public bool hasPlayerDied;
    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject playerCamera;

    [Header("Player Keys")]
    public bool hasKey;

    [Header("Audio Cues")]
    public bool heavyMusic;
    private float timerUntilMusicStops;

    [Header("Player Audio")]

    public AudioManager audioManager;

    private void Update() {
        checkEnemyCounter();

        if(heavyMusic && enemiesAggroed < 5) {
            timerUntilMusicStops += Time.deltaTime;
        }

        if(enemiesAggroed != 0){
            heavyMusic = true;
        }
        else heavyMusic = false;

        if(health <= 0 && !hasPlayerDied) {
            hasPlayerDied = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        player.SetActive(false);
        playerCamera.GetComponent<CameraController>().enabled = false;
        playerCamera.transform.GetChild(2).gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;

        audioManager.PlaySelectedGeneralRandom(1);
    }

    public void DamageMe(int damage) {
        if (armor == 0) {
            health -= damage;
            if (health < 0) {
                health = 0;
            }

            audioManager.PlaySelectedGeneralRandom(0);

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

        audioManager.PlaySelectedGeneralRandom(0);
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

    private void checkEnemyCounter() {
        if(enemiesAggroed >= 5) {
            Debug.Log("HEAVY MUSIC STARTS");
            heavyMusic = true;
        } else if(enemiesAggroed < 5 && timerUntilMusicStops > 5.0f) {
            Debug.Log("CALM MUSIC STARTS");
            heavyMusic = false;
            timerUntilMusicStops = 0.0f;
        }
    }
}