using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {
    [Header("General Enemy Stats")]
    public int enemyHealth;
    public bool isEnemyAggroed, isDead;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform orientation;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;
    public Door killDoor;
    [SerializeField] private int maxHealth;
    private GameObject playerObject;
    public PlayerSystem playerSystem;

    // Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks, attackTimer;
    public bool flyingEnemy, meleeEnemy;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, angry, canChase, attacking;

    private float deathTimer;
    private bool inEnemyCounter;

    [Header("Enemy Audio")]

    public AudioEnemies audioEnemies;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        maxHealth = enemyHealth;

        GameManager.OnPlayerInstantiated += HandlePlayerInstantiated;

        // Check if the player is already cached
        if (GameManager.CachedPlayerInstance != null)
        {
           HandlePlayerInstantiated(GameManager.CachedPlayerInstance);
        }
    }

    private void Update() {
        UpdateState();
        HandleDeathTimer();

        if(angry) {
            attackTimer += Time.deltaTime;
        }

        if(attackTimer >= timeBetweenAttacks && !attacking) {
            attacking = true;
        }
    }

    private void HandlePlayerInstantiated(GameObject playerInstance)
    {
        playerObject = playerInstance;
        player = playerObject.transform.GetChild(0).transform;
        playerSystem = playerObject.transform.GetChild(0).GetComponent<PlayerSystem>();
    }

    private void UpdateState() {
        playerInSightRange = CheckLineOfSight(sightRange);
        playerInAttackRange = CheckLineOfSight(attackRange);
        isEnemyAggroed = enemyHealth < maxHealth || playerInSightRange;

        if (isDead) return;

        if(meleeEnemy) {
            if (!angry) Idle();
            if (angry && !canChase) Patrolling();
            if (angry && canChase && !playerInAttackRange) Chase();
            if (angry && playerInAttackRange && playerInSightRange) Attack();
        } else {
            if (!angry) Idle();
            if (angry && !canChase) Patrolling();
            if (angry && canChase && !attacking) Chase();
            if (angry && attacking && playerInSightRange) Attack();
        }

        if (isEnemyAggroed && !inEnemyCounter) {
            UpdateAggroedCounter();
            angry = true;
        }

        if(angry) {
            Vector3 targetPosition = ProjectToNavMesh(player.position);
            if (targetPosition != Vector3.zero && IsDestinationReachable(targetPosition)) {
                canChase = true;
            } else {
                canChase = false;
            }
        }
    }

    private bool CheckLineOfSight(float range)
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Cast a ray toward the player
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, range))
        {
            // Check if the ray hits the player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
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
        navMeshAgent.SetDestination(player.position);
    }

    public virtual void Attack() {
        // Implementation to be provided by derived classes
    }

    public void TakeDamage(int damage) {
        enemyHealth -= damage;

        if (enemyHealth <= 0 && !isDead) {
            HandleDeath();
        }
        else{
            audioEnemies.playThisSound(2);
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
        playerSystem.enemiesAggroed--;

        animator.SetTrigger("Death");
        audioEnemies.playThisSound(3);

        if(killDoor != null) killDoor.enemiesToKill--;
    }

    public void ResetAttack() {
        navMeshAgent.isStopped = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Patrol", true);
        attackTimer = 0.0f;
        attacking = false;
    }

    private void UpdateAggroedCounter() {
        playerSystem.enemiesAggroed++;
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