using UnityEngine;

public class KeyPickup : MonoBehaviour {
    [Header("Specific Stats")]
    public PlayerSystem player;
    public CanvasController playerHUD;
    public KeyColor keyColor;
    public enum KeyColor{
        red,
        blue,
        yellow
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        player.hasKey = true;
        playerHUD.SetStatusText("You got the key!");
        playerHUD.EnableKey();

        Destroy(gameObject);
    }
}