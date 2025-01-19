using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Stats")]
    public PlayerSystem player;
    public int damage;
    public float lifeSpan;
    public bool affectedByGravity;
    public bool destroyOnImpact;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
    }

    private void Start() {
        Destroy(gameObject, lifeSpan);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            player.DamageMe(damage);
        }
        if(destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}
