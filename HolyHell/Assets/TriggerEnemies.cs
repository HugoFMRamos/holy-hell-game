using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    
    private void OnTriggerEnter(Collider other) {
        if(enemiesToSpawn.Length != 0) {
            foreach(GameObject gameObject in enemiesToSpawn) {
                gameObject.SetActive(true);
            }
        }
    }
}
