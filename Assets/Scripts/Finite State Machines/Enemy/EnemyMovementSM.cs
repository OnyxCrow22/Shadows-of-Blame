using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSM : EnemyStateMachine
{
    public bool isPatrol = false;
    public bool attacking = false;
    public bool shoot = false;
    public bool dealDamage = false;
    public Transform target;
    public Animator eAnim;
    public float damage;
    public float attackDelay = 1.2f;
    public Transform enemy;
    public Transform[] waypoints;
    public Transform ePoint;
    public float distance;
    public PlayerMovementSM playsm;
    public NavMeshAgent agent;
    [HideInInspector]
    public int destinations;

    // Scripts
    public PlayerHealth health;
    public EnemyHealth eHealth;
    public EnemyFOV eFOV;

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
}
