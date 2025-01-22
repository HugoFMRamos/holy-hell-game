using UnityEngine;

public class HealthPickup : PickUp {
    [Header("Specific Stats")]
    public PlayerSystem player;

    private void Start() {
        GameManager.OnPlayerInstantiated += HandlePlayerInstantiated;

        // Check if the player is already cached
        if (GameManager.CachedPlayerInstance != null)
        {
            HandlePlayerInstantiated(GameManager.CachedPlayerInstance);
        }
    }

    private void HandlePlayerInstantiated(GameObject playerInstance)
    {
        playerObject = playerInstance;
        player = playerObject.transform.GetChild(0).GetComponent<PlayerSystem>();
        playerHUD = playerObject.transform.GetChild(1).GetComponent<CanvasController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if (player.health < player.maxHealth) {
            player.HealMe(base.valueToAdd, true);
        }

        string text = "Health! (+" + base.valueToAdd + ")";
        playerHUD.SetMiniStatusText(text);
        Destroy(gameObject);
    }
}