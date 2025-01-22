using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance; // Singleton instance
    private Transform spawnPoint;
    public GameObject playerPrefab;
    public static GameObject CachedPlayerInstance { get; private set; }
    public PlayerData playerData = new(0, 0, 0, new List<int>(), new List<string>());
    public static event Action<GameObject> OnPlayerInstantiated;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        if(SceneManager.GetActiveScene().buildIndex != 0) {
            Debug.Log($"New Scene Loaded: {scene.name}");
            spawnPoint = GameObject.Find("Spawnpoint").GetComponent<Transform>();
            SpawnPlayer();
        } else {
            ResetPlayerData();
        }
    }

    public void SpawnPlayer() {
        GameObject playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        PlayerSystem playerSystem = playerObject.transform.GetChild(0).GetComponent<PlayerSystem>();
        Inventory inventory = playerObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();
        LoadPlayerData(playerSystem, inventory);
        CachedPlayerInstance = playerObject;

        OnPlayerInstantiated?.Invoke(playerObject);
    }

    public void ResetPlayerData()
    {
        // Reset stats if needed after a true game over
        playerData = new(0, 0, 0, new List<int>(), new List<string>());
    }

    public void LoadPlayerData(PlayerSystem player, Inventory inventory)
    {
        if(playerData.Health == 0) {
            return;
        }

        player.health = playerData.Health;
        player.armor = playerData.Armor;

        for(int i = 0; i < playerData.WeaponsInInventory.Count; i++) {
            Weapon weapon = inventory.weaponList[i].GetComponent<Weapon>();
            if(weapon.name.Equals(playerData.WeaponsInInventory[i])) {
                weapon.ammo = playerData.WeaponAmmoList[i];
                weapon.isInInventory = true;
            }
        }

        inventory.SwitchWeapon(playerData.CurrentWeapon);
    }

    public void SavePlayerStats(PlayerSystem player, Inventory inventory)
    {
        // Save current player stats to the data class
        playerData.Health = player.health;
        playerData.Armor = player.armor;
        playerData.CurrentWeapon = inventory.currentWeapon;

        for(int i = 0; i < inventory.weaponList.Count; i++) {
            Weapon weapon = inventory.weaponList[i].GetComponent<Weapon>();
            if(weapon.isInInventory) {
                playerData.WeaponAmmoList.Add(weapon.ammo);
                playerData.WeaponsInInventory.Add(weapon.name);
            }
        }
    }
}
