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

        if(keyColor == KeyColor.red) {
            player.hasRedKey = true;
            playerHUD.SetStatusText("You got the RED key!");
            playerHUD.EnableKey(0);
        } else if(keyColor == KeyColor.blue) {
            player.hasBlueKey = true;
            playerHUD.SetStatusText("You got the BLUE key!");
            playerHUD.EnableKey(1);
        } else if(keyColor == KeyColor.yellow) {
            player.hasYellowKey = true;
            playerHUD.SetStatusText("You got the YELLOW key!");
            playerHUD.EnableKey(2);
        }

        Destroy(gameObject);
    }
}