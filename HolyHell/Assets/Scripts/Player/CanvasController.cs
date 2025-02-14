using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    public PlayerController playerController;
    public PlayerSystem playerSystem;
    public CameraController cameraController;
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
    public GameObject menuScreen;
    public GameObject key;
    public List<GameObject> selectWeaponImages;
    public List<GameObject> invWeaponImages;

    private GameObject activeWeapon;
    private GameObject inventory;
    public float statusTimer;
    public float statusTime = 3.0f;
    private float miniStatusTimer, xSens, ySens;
    private bool statusOn, miniStatusOn;
    public bool isMenuActive;

    private void Awake() {
        inventory = GameObject.Find("Inventory");
        playerSystem = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraController = GameObject.Find("PlayerCamera").GetComponent<CameraController>();
    }

    private void Start() {
        xSens = cameraController.xSensitivity;
        ySens = cameraController.ySensitivity;

        cameraController.xSensitivity = xSens;
        cameraController.ySensitivity = ySens;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventory.SetActive(true);
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuScreen.SetActive(!menuScreen.activeSelf);
            isMenuActive = menuScreen.activeSelf;
            cameraController.xSensitivity = isMenuActive ? 0.0f : xSens;
            cameraController.ySensitivity = isMenuActive ? 0.0f : ySens;
            Time.timeScale = isMenuActive ? 0.0f : 1.0f;
            Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isMenuActive;
            inventory.SetActive(!isMenuActive);
        }

        if(playerSystem.gameOverScreen.activeSelf == true && Input.GetKeyDown(KeyCode.E)) {
            RestartLevel();
        }


        SetPlayerHealth();
        SetPlayerArmor();

        FindCurrentActiveWeapon();
        if (activeWeapon != null)
        {
            SetAmmoText();
            SetWeaponIcon();
        }

        if(statusOn) statusTimer += Time.deltaTime;
        if(statusTimer > statusTime) {
            ResetStatusText();
            statusOn = false;
        }

        if(miniStatusOn) miniStatusTimer += Time.deltaTime;
        if(miniStatusTimer > statusTime) {
            ResetMiniStatusText();
            miniStatusOn = false;
        }

        //Debug.Log(isMenuActive);
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

    private void ResetMiniStatusText() {
        miniStatusText.text = "";
    }

    private void SetAmmoText() {
        Weapon weapon = activeWeapon.GetComponent<Weapon>();
        if(weapon.startAmmo <= -1) {
            ammoText.text = "- | ∞";
        } else {
            ammoText.text = weapon.ammo + " | " + weapon.maxAmmo;
        }
        weaponNameText.text = weapon.name;
    }

    private void SetWeaponIcon() {
        Weapon weapon = activeWeapon.GetComponent<Weapon>();
        foreach(GameObject gameObject in selectWeaponImages) {
            if(gameObject.name.Equals(weapon.name)) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetWeaponsAvailable() {
        for(int i = 0; i < weaponList.Count; i++) {
            Weapon weapon = weaponList[i].GetComponent<Weapon>();
            if(weapon.isInInventory) {
                RawImage image = invWeaponImages[i].GetComponent<RawImage>();
                image.color = new Color(1, 1, 1);
            }
        }
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

    public void EnableKey() {
        RawImage keyImage = key.GetComponent<RawImage>();
        keyImage.color = new Color(1,1,1,1);
    }

    public void ResumeGame() {
        menuScreen.SetActive(!menuScreen.activeSelf);
        bool isMenuActive = menuScreen.activeSelf;
        cameraController.xSensitivity = isMenuActive ? 0.0f : xSens;
        cameraController.ySensitivity = isMenuActive ? 0.0f : ySens;
        Time.timeScale = isMenuActive ? 0.0f : 1.0f;
        Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuActive;
        inventory.SetActive(!isMenuActive);
    }

    public void RestartLevel() {
        GameObject player = GameObject.Find("PlayerHolder_v2");
        if (player != null)
        {
            PlayerSystem playerSystem = player.transform.GetChild(0).GetComponent<PlayerSystem>();
            Inventory inventory = player.transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();
            gameManager.SavePlayerStats(playerSystem, inventory);
            
            Destroy(player);
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame() {
        SceneManager.LoadScene(0);
    }
}
