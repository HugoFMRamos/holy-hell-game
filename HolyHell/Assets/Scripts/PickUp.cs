using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("General Pick Up Stats")]
    public int valueToAdd;
    public CanvasController playerHUD;

    private void Awake() {
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<CanvasController>();
    }
}
