using UnityEngine;

public class KeyPickup : MonoBehaviour {
    [Header("Specific Stats")]
    public PlayerSystem player;
    public CanvasController playerHUD;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        player.hasKey = true;
        playerHUD.SetStatusText("You got the key!");
        playerHUD.EnableKey();

        Destroy(gameObject);
    }
}