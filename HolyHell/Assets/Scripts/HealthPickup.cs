using UnityEngine;

public class HealthPickup : PickUp {
    [Header("Specific Stats")]
    public PlayerSystem player;

    private void OnTriggerEnter(Collider other) {
        if(player.health < player.maxHealth) {
            player.HealMe(base.valueToAdd, true);
            Destroy(gameObject);
        }
    }
}