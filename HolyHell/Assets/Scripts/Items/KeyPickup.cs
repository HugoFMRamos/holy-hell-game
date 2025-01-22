using UnityEngine;

public class KeyPickup : MonoBehaviour {
    [Header("Specific Stats")]
    public PlayerSystem player;
    public CanvasController playerHUD;
    private GameObject playerObject;

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

        player.hasKey = true;
        playerHUD.SetStatusText("You got the key!");
        playerHUD.EnableKey();

        Destroy(gameObject);
    }
}