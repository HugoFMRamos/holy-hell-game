using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGate : MonoBehaviour
{
    [Header("Details")]
    public PlayerSystem player;
    public CanvasController playerHUD;
    public bool needsKey;
    private bool checkInput;
    private bool entryText = true;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
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
            SceneManager.LoadScene("MainMenuScene");
        }

        if(playerHUD.statusTimer > playerHUD.statusTime && checkInput == true) {
            entryText = true;
        }
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
