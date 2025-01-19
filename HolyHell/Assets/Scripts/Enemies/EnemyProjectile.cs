using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Stats")]
    public PlayerSystem player;
    public GameObject explosionEffect;
    public LayerMask whatIsHitOnExplode;
    public int damage;
    public float lifeSpan;
    public float explosionRadius;
    public bool affectedByGravity;
    public bool canExplode;
    public bool destroyOnImpact;
    private Rigidbody rb;
    private float timer;
    private bool hasExploded;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerSystem>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = affectedByGravity;
    }

    private void Start() {
        Destroy(gameObject, lifeSpan);
        timer = lifeSpan;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if(timer < 0f) {
            if(canExplode && !hasExploded) {
                Explode();
                hasExploded = true;
            } else if(!canExplode){
                Destroy(gameObject);
            }
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, whatIsHitOnExplode);
        foreach(Collider collider in hits) {
            Debug.Log($"Detected: {collider.gameObject.name}");
            PlayerSystem player = collider.gameObject.GetComponent<PlayerSystem>();
            if(player != null) {
                player.DamageMe(damage);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        PlayerSystem player = other.gameObject.GetComponent<PlayerSystem>();
        if(player != null) {
            if(canExplode && !hasExploded) {
                Explode();
                hasExploded = true;
            } else {
                player.DamageMe(damage);
            }
            Destroy(gameObject);
        }

        if(destroyOnImpact) {
            if(canExplode && !hasExploded) {
                Explode();
                hasExploded = true;
            }
            Destroy(gameObject);
        }
    }
}
