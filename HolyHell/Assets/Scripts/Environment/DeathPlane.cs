using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponent<PlayerSystem>().GameOver();
        }
    }
}
