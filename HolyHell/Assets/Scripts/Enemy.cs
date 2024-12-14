using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {
    [Header("General Enemy Stats")]
    public int enemyHealth;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform orientation;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake() {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) Chase();
        if(playerInAttackRange && playerInSightRange) Attack();
    }

    private void Patrolling() {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet) {
            Debug.Log("Patrolling!");
            navMeshAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void Chase() {
        Debug.Log("Chasing!");
        navMeshAgent.SetDestination(player.position);
    }

    public virtual void Attack() {
        
    }

    public void ResetAttack() {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage) {
        enemyHealth -= damage;

        if(enemyHealth <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }
}