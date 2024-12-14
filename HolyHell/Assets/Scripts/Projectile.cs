using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float forwardSpeed;
    public float upwardSpeed;
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
        rb.AddForce(rb.transform.forward * forwardSpeed, ForceMode.Impulse);
        rb.AddForce(rb.transform.up * upwardSpeed, ForceMode.Impulse);
        Destroy(gameObject, lifeSpan);
    }

    private void OnCollisionEnter(Collision other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null) {
            enemy.TakeDamage(damage);
        }

        if(destroyOnImpact) {
            Destroy(gameObject);
        }
    }
}
