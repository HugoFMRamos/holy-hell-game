using UnityEngine;

public class EnemyTypeMelee : Enemy {
    [Header("Enemy Melee Stats")]
    public BoxCollider damageCollider;
    public int damage;

    public override void Attack() {
        //Make sure enemy doesn't move
        navMeshAgent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if(!alreadyAttacked) {

            damageCollider.enabled = true;

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            player.GetComponent<PlayerSystem>().DamageMe(damage);
        }
        damageCollider.enabled = false;
    }
}