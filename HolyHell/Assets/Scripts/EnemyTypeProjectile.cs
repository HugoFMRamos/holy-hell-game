using UnityEngine;

public class EnemyTypeProjectile : Enemy {
    [Header("Enemy Projectile Stats")]
    public GameObject enemyProjectile;
    public float projectileForwardSpeed;
    public float projectileUpwardSpeed;

    public override void Attack() {
        animator.SetBool("Attack", true);

        //Make sure enemy doesn't move
        navMeshAgent.isStopped = true;
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
    }

    public void attackCalled() {
        Vector3 directionToPlayer = (player.position - orientation.position).normalized;
        Rigidbody rb = Instantiate(enemyProjectile, orientation.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(directionToPlayer * projectileForwardSpeed, ForceMode.Impulse);
        rb.AddForce(orientation.up * projectileUpwardSpeed, ForceMode.Impulse);
    }
}