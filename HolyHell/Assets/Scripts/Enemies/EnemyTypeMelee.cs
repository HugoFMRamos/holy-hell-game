using UnityEngine;

public class EnemyTypeMelee : Enemy {
    [Header("Enemy Melee Stats")]
    public BoxCollider damageCollider;
    public int damage;

    public override void Attack() {
        animator.SetBool("Attack", true);

        //Make sure enemy doesn't move
        navMeshAgent.isStopped = true;
        transform.LookAt(player);
    }

    public void attackCalled() {
        damageCollider.enabled = true;
        
        if (!playerInAttackRange) {
            damageCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !isDead) {
            player.GetComponent<PlayerSystem>().DamageMe(damage);
            damageCollider.enabled = false;
        }
    }
}