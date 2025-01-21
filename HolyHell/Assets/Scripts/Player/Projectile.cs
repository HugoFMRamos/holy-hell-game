using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float forwardSpeed;
    public float upwardSpeed;
    public float lifeSpan;
    public float explosionRadius;
    public bool affectedByGravity;
    public bool destroyOnImpact;
    public bool canExplode;
    public LayerMask whatIsHitOnExplode;
    public Transform orientation;
    public GameObject explosionEffect;
    private Rigidbody rb;
    private float timer;
    private bool hasExploded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = affectedByGravity;
    }

    private void Start() {
        rb.AddForce(rb.transform.forward * forwardSpeed, ForceMode.Impulse);
        rb.AddForce(rb.transform.up * upwardSpeed, ForceMode.Impulse);
        timer = lifeSpan;

    }

    private void Update() {
        timer -= Time.deltaTime;
        if(timer < 0f) {
            if(canExplode && !hasExploded) {
                Explode();
                hasExploded = true;
            }
            Destroy(gameObject);
        }
    }

    private void Explode()
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, whatIsHitOnExplode);
        foreach(Collider collider in hits) {
            Debug.Log($"Detected: {collider.gameObject.name}");
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            PlayerSystem player = collider.gameObject.GetComponent<PlayerSystem>();
            if(enemy != null) {
                enemy.TakeDamage(damage);
            } else if(player != null) {
                player.DamageMe(damage);
            }
        }

        
    }

    private void OnCollisionEnter(Collision other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null) {
            if(canExplode && !hasExploded) {
                Explode();
                hasExploded = true;
            } else {
                enemy.TakeDamage(damage);
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
