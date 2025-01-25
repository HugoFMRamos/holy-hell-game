using UnityEngine;

public class ArmorPickup : PickUp {
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

        if (player.armor < player.maxArmor) {
            player.HealMe(base.valueToAdd, false);

            AudioSource pickUpSource = playerHUD.transform.GetChild(9).GetComponent<AudioSource>();
            pickUpSource.clip = pickupSound;
            pickUpSource.Play();

            string text = "Armor! (+" + base.valueToAdd + ")";
            playerHUD.SetMiniStatusText(text);
            Destroy(gameObject);
        }
    }
}