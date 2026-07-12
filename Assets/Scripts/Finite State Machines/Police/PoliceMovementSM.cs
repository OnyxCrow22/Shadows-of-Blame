using UnityEngine;
using UnityEngine.AI;

public class PoliceMovementSM : PoliceStateMachine
{
    [Header("References")]
    public NavMeshAgent PoliceAI;
    public GameObject player;
    public Transform officer;
    public PlayerMovementSM playsm;
    public Animator PoliceAnim;
    public GameObject PoliceFOV;
    public GameObject pGun;
    public PoliceGun policeGun;
    public FollowWaypoints follow;
    public HealthSystem pHealth;

    // Singleton or persistent reference for performance
    [HideInInspector]
    public PoliceLevel policing;

    [Header("State Flags")]
    public bool isPatrolling = false;
    public bool isChasing = false;
    public bool isShooting = false;

    [HideInInspector] public PoliceIdle idleState;
    [HideInInspector] public PolicePatrol patrolState;
    [HideInInspector] public PoliceChase chaseState;
    [HideInInspector] public PoliceShoot fireState;

    private void Awake()
    {
        idleState = new PoliceIdle(this);
        patrolState = new PolicePatrol(this);
        chaseState = new PoliceChase(this);
        fireState = new PoliceShoot(this);

        // Direct reference - No search operations, no deprecation warnings
        policing = PoliceLevel.Instance;
    }

    protected override PoliceBaseState GetInitialState()
    {
        return idleState;
    }
}