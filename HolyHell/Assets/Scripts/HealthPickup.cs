using UnityEngine;

public class HealthPickup : PickUp {
    [Header("Specific Stats")]
    public PlayerSystem player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if (player.health < player.maxHealth) {
            player.HealMe(base.valueToAdd, true);
        }

        string text = "+" + base.valueToAdd + " health!";
        playerHUD.SetStatusText(text);
        Destroy(gameObject);
    }
}