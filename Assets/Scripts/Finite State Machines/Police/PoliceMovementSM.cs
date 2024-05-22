using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceMovementSM : PoliceStateMachine
{
    public NavMeshAgent PoliceAI;
    public GameObject player;
    public Transform officer;
    public PlayerMovementSM playsm;
    public RemoveNPC removed;
    public Animator PoliceAnim;
    public GameObject PoliceFOV;
    public GameObject pGun;

    public PoliceGun policeGun;
    public FollowWaypoints follow;

    public bool spawnedIn = false;
    public bool isPatrolling = false;
    public bool isChasing = false;
    public bool isAttacking = false;
    public bool isShooting = false;

    [HideInInspector]
    public PoliceIdle idleState;
    [HideInInspector]
    public PolicePatrol patrolState;
    [HideInInspector]
    public PoliceChase chaseState;
    [HideInInspector]
    public PoliceShoot fireState;
   // [HideInInspector]
   // public PoliceAttack meleeState;
    private void Awake()
    {
        idleState = new PoliceIdle(this);
        patrolState = new PolicePatrol(this);
        chaseState = new PoliceChase(this);
        fireState = new PoliceShoot(this);
       // meleeState = new PoliceAttack(this);
    }

    protected override PoliceBaseState GetInitialState()
    {
        return idleState;
    }
}
