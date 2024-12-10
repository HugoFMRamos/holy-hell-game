using UnityEngine;

public class ArmorPickup : PickUp {
    [Header("Specific Stats")]
    public PlayerSystem player;

    private void OnTriggerEnter(Collider other) {
        if(player.armor < player.maxArmor) {
            player.HealMe(base.valueToAdd, false);
            Destroy(gameObject);
        }
    }
}