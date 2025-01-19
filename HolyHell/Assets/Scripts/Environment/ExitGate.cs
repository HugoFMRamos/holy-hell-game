using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private bool checkInput;
    public CanvasController playerHUD;

    private void Awake() {
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && checkInput) {
            Debug.Log("YOU EXITED THE LEVEL!");
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            playerHUD.SetStatusText("Press E to exit the level");
            checkInput = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            playerHUD.SetStatusText("");
            checkInput = false;
        }
    }
}
