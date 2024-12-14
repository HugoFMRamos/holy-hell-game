using UnityEngine;

public class EnemyTypeProjectile : Enemy {
    [Header("Enemy Projectile Stats")]
    public GameObject enemyProjectile;
    public float projectileForwardSpeed;
    public float projectileUpwardSpeed;

    public override void Attack() {
        Debug.Log("Attacking!");
        //Make sure enemy doesn't move
        navMeshAgent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if(!alreadyAttacked) {

            Rigidbody rb = Instantiate(enemyProjectile, orientation.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(orientation.forward * projectileForwardSpeed, ForceMode.Impulse);
            rb.AddForce(orientation.up * projectileUpwardSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
}