using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGate : MonoBehaviour
{
    [Header("Details")]
    public GameManager gameManager;
    public PlayerSystem player;
    public CanvasController playerHUD;
    public int nextScene;
    public bool needsKey;
    private bool checkInput;
    private bool entryText = true;
    private GameObject playerObject;

    private void Start() {
        GameManager.OnPlayerInstantiated += HandlePlayerInstantiated;

        // Check if the player is already cached
        if (GameManager.CachedPlayerInstance != null)
        {
            HandlePlayerInstantiated(GameManager.CachedPlayerInstance);
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && checkInput) {
            entryText = false;
            if (needsKey) {
                if (!player.hasKey) {
                    playerHUD.SetStatusText("You need a key to exit!");
                    return;
                }
            }

            LoadNextScene();
        }

        if(playerHUD.statusTimer > playerHUD.statusTime && checkInput == true) {
            entryText = true;
        }
    }

    private void HandlePlayerInstantiated(GameObject playerInstance)
    {
        playerObject = playerInstance;
        player = playerObject.transform.GetChild(0).GetComponent<PlayerSystem>();
        playerHUD = playerObject.transform.GetChild(1).GetComponent<CanvasController>();
    }

    private void LoadNextScene() {
        if (playerObject != null)
        {
            Inventory inventory = playerObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Inventory>();
            gameManager.SavePlayerStats(player, inventory);
            
            Destroy(playerObject);
        }
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag != "Player") return;
        
        checkInput = true;
        if(entryText) {
            playerHUD.SetStatusText("Press E to exit the level");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            playerHUD.SetStatusText("");
            checkInput = false;
            entryText = true;
        }
    }
}
