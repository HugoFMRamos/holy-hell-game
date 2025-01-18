using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {
    [Header("General Enemy Stats")]
    public int enemyHealth;
    public bool isEnemyAggroed, isDead, detectedWeaponSound = false;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform orientation;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    public AnimatorStateInfo animatorState;
    private float deathTimer;
    [SerializeField] private int maxHealth;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked, flyingEnemy;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake() {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        maxHealth = enemyHealth;
    }

    private void Update() {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isEnemyAggroed = enemyHealth < maxHealth || detectedWeaponSound || playerInSightRange;

        if(!isEnemyAggroed && !playerInSightRange && !playerInAttackRange && !isDead) Idle();
        if(isEnemyAggroed && !playerInSightRange && !playerInAttackRange && !isDead) Patrolling();
        if(isEnemyAggroed && playerInSightRange && !playerInAttackRange && !isDead) Chase();
        if(isEnemyAggroed && playerInAttackRange && playerInSightRange && !isDead) Attack();

        if(isDead) {
            deathTimer += Time.deltaTime;
        }
        if(deathTimer > 10.0f) {
            DestroyEnemy();
        }
    }

    private void Idle() {
        animator.SetBool("Idle", true);
    }

    private void Patrolling() {
        animator.SetBool("Patrol", true);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            navMeshAgent.SetDestination(walkPoint);
        } else {
            // If the walk point is unreachable, reset it and search for a new one
            walkPointSet = false;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        if(!flyingEnemy) {
            walkPoint = ProjectToNavMesh(new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ));
        } else {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        }

        if(IsDestinationReachable(walkPoint)) {
            walkPointSet = true;
        }
    }

    private void Chase() {
        animator.SetBool("Patrol", true);
        Vector3 targetPosition = ProjectToNavMesh(player.position);

        if (targetPosition != Vector3.zero && IsDestinationReachable(targetPosition)) {
            navMeshAgent.SetDestination(player.position);
        } else {
            Patrolling();
        }
    }

    public virtual void Attack() {
        
    }

    private Vector3 ProjectToNavMesh(Vector3 position) {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 5f, NavMesh.AllAreas)) {
            return hit.position; // Return the nearest point on the NavMesh
        }
        return Vector3.zero; // Return zero vector if no valid position is found
    }

    private bool IsDestinationReachable(Vector3 destination) {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(destination, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public void ResetAttack() {
        alreadyAttacked = false;
        navMeshAgent.isStopped = false;
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(int damage) {
        enemyHealth -= damage;

        if(enemyHealth <= 0) {
            isDead = true;
            navMeshAgent.isStopped = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
            animator.SetTrigger("Death");
        }
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }
}