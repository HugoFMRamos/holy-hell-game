using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Details")]
    public PlayerSystem player;
    public CanvasController playerHUD;
    public bool killDoor;
    public int enemiesToKill;
    private bool checkInput;
    private bool entryText = true;

    public GameObject doorOpen;
    public GameObject doorClose;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }

    private void Update() {
        if(!killDoor && Input.GetKeyDown(KeyCode.E) && checkInput) {
            entryText = false;
            if(!player.hasKey) {
                playerHUD.SetStatusText("You need a key!");
                return;
            }
            playerHUD.SetStatusText("");
            if(doorOpen != null & doorClose != null){
                doorClose.SetActive(false);
                doorOpen.SetActive(true);
            }
            Destroy(gameObject);
        } else if (killDoor && enemiesToKill == 0) {
            playerHUD.SetStatusText("A door has opened!");
            Destroy(gameObject);
        }

        if(playerHUD.statusTimer > playerHUD.statusTime && checkInput == true) {
            entryText = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag != "Player") return;
        
        checkInput = true;
        if(entryText) {
            if(killDoor) {
                playerHUD.SetStatusText("This door needs more souls");
            } else {
                playerHUD.SetStatusText("Press E to open the door");
            }
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
