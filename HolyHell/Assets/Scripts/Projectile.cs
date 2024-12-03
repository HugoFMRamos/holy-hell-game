using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float speed;
    public float lifeSpan;
    public bool affectedByGravity;
    public bool destroyOnImpact;
    public Transform orientation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = affectedByGravity;
    }

    private void Start() {
        rb.AddForce(rb.transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, lifeSpan);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag != "Player" && destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}
