using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;

    [Header("Canvas Elements")]
    public TextMeshProUGUI speedText;

    private void Update() {
        speedText.text = "Speed: " + playerController.currentSpeed;
    }
}
