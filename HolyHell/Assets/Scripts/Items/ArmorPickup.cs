using UnityEngine;

public class ArmorPickup : PickUp {
    [Header("Specific Stats")]
    public PlayerSystem player;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if (player.armor < player.maxArmor) {
            player.HealMe(base.valueToAdd, false);
        }

        string text = "Armor! (+" + base.valueToAdd + ")";
        playerHUD.SetMiniStatusText(text);
        Destroy(gameObject);
    }
}