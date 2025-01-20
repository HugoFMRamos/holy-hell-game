using UnityEditor.Callbacks;
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
    public Door killDoor;

    [SerializeField] private int maxHealth;

    // Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked, flyingEnemy;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private float deathTimer;
    private bool inEnemyCounter;

    private void Awake() {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        maxHealth = enemyHealth;
    }

    private void Update() {
        UpdateState();
        HandleDeathTimer();
    }

    private void UpdateState() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isEnemyAggroed = enemyHealth < maxHealth || detectedWeaponSound || playerInSightRange;

        if (isDead) return;

        if (!isEnemyAggroed && !playerInSightRange && !playerInAttackRange) Idle();
        else if (isEnemyAggroed && !playerInSightRange && !playerInAttackRange) Patrolling();
        else if (isEnemyAggroed && playerInSightRange && !playerInAttackRange) Chase();
        else if (isEnemyAggroed && playerInAttackRange) Attack();

        if (isEnemyAggroed && !inEnemyCounter) UpdateAggroedCounter();
    }

    private void HandleDeathTimer() {
        if (isDead) {
            deathTimer += Time.deltaTime;
            if (deathTimer > 10.0f) Destroy(gameObject);
        }
    }

    private void Idle() {
        animator.SetBool("Idle", true);
        animator.SetBool("Patrol", false);
    }

    private void Patrolling() {
        animator.SetBool("Patrol", true);
        animator.SetBool("Idle", false);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            navMeshAgent.SetDestination(walkPoint);
            if (Vector3.Distance(transform.position, walkPoint) < 2f) walkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 potentialPoint = flyingEnemy 
            ? new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ) 
            : ProjectToNavMesh(new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ));

        if (IsDestinationReachable(potentialPoint)) {
            walkPoint = potentialPoint;
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
        // Implementation to be provided by derived classes
    }

    public void TakeDamage(int damage) {
        enemyHealth -= damage;

        if (enemyHealth <= 0 && !isDead) {
            HandleDeath();
        }
    }

    private void HandleDeath() {
        isDead = true;
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        if(!flyingEnemy) {
            GetComponent<CapsuleCollider>().isTrigger = true;
        } else {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        player.gameObject.GetComponent<PlayerSystem>().enemiesAggroed--;
        animator.SetTrigger("Death");

        if(killDoor != null) killDoor.enemiesToKill--;
    }

    public void ResetAttack() {
        alreadyAttacked = false;
        navMeshAgent.isStopped = false;
        animator.SetBool("Attack", false);
    }

    private void UpdateAggroedCounter() {
        player.gameObject.GetComponent<PlayerSystem>().enemiesAggroed++;
        inEnemyCounter = true;
    }

    private Vector3 ProjectToNavMesh(Vector3 position) {
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5f, NavMesh.AllAreas)) {
            return hit.position;
        }
        return Vector3.zero;
    }

    private bool IsDestinationReachable(Vector3 destination) {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(destination, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
}