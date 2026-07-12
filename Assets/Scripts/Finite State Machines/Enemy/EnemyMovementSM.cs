using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSM : EnemyStateMachine
{
    [Header("Boolean References")]
    public bool isPatrol = false;
    public bool isChasing = false;
    public bool isMeleeAttack = false;
    public bool isShooting = false;
    public bool isDealDamage = false;
    public bool isHiding = false;
    public bool isAttacking = false;
    public bool attackedPlayer = false;

    [Header("Transform References")]
    public Transform target;
    public Transform enemy;
    public Transform[] waypoints;
    public Transform ePoint;

    [Header("Float/int references")]
    public float damage;
    public float attackDelay = 1.2f;
    public float distance;
    [HideInInspector]
    public int destinations;
    public int RandomIndex;
    [Range(-1, 1)]
    public float HideSensitvity;

    [Header("Other references")]
    public Animator eAnim;
    public GameObject FOV;
    public NavMeshAgent agent;
    [HideInInspector]
    public Collider coverObj;
    public Collider[] cols = new Collider[20]; // Pre-allocated size buffer to prevent memory leaks during NonAlloc loops
    public LayerMask hideableLayers, Player;

    [Header("External Scripts")]
    public PlayerMovementSM playsm;
    public HealthSystem eHealth;
    public HealthSystem playerHealth;
    public EnemyCoverSystem eCover;
    public EnemyMeleeSystem eMSystem;
    public EnemyGun eGun;

    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public EnemyPatrol patrolState;
    [HideInInspector]
    public EnemyChase chaseState;
    [HideInInspector]
    public EnemyShoot fireState;
    [HideInInspector]
    public EnemyMelee meleeState;
    [HideInInspector]
    public EnemyCover coverState;

    private void Awake()
    {
        idleState = new EnemyIdle(this);
        patrolState = new EnemyPatrol(this);
        chaseState = new EnemyChase(this);
        fireState = new EnemyShoot(this);
        meleeState = new EnemyMelee(this);
        coverState = new EnemyCover(this);
    }

    protected override EnemyBaseState GetInitialState()
    {
        return idleState;
    }

    public void RandomIndexCheck()
    {
        if (cols == null || cols.Length == 0) return;
        RandomIndex = Random.Range(0, cols.Length);
        coverObj = cols[RandomIndex];
    }

    public int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null) return 1;
        if (A != null && B == null) return -1;
        if (A == null && B == null) return 0;

        // Compares the distance between Collider A and B.
        return Vector3.Distance(agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(agent.transform.position, B.transform.position));
    }
}