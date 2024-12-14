using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Details")]
    public PlayerSystem player;
    public KeyNeeded keyNeeded;
    public enum KeyNeeded{
        red,
        blue,
        yellow
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") return;

        if(keyNeeded == KeyNeeded.red && player.hasRedKey) {
            Destroy(gameObject);
        } else if(keyNeeded == KeyNeeded.blue && player.hasBlueKey) {
            Destroy(gameObject);
        } else if(keyNeeded == KeyNeeded.yellow && player.hasYellowKey) {
            Destroy(gameObject);
        }
    }
}
