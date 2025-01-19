using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Details")]
    public PlayerSystem player;
    public CanvasController playerHUD;
    public KeyNeeded keyNeeded;
    public enum KeyNeeded{
        red,
        blue,
        yellow
    }

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if(keyNeeded == KeyNeeded.red) {
            if(!player.hasRedKey) {
                playerHUD.SetStatusText("You need the RED key to open this door!");
                return;
            }
            Destroy(gameObject);
        } else if(keyNeeded == KeyNeeded.blue) {
            if(!player.hasRedKey) {
                playerHUD.SetStatusText("You need the BLUE key to open this door!");
                return;
            }
            Destroy(gameObject);
        } else if(keyNeeded == KeyNeeded.yellow) {
            if(!player.hasYellowKey) {
                playerHUD.SetStatusText("You need the YELLOW key to open this door!");
                return;
            }
            Destroy(gameObject);
        }
    }
}
