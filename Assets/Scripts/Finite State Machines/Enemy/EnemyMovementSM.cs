using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSM : EnemyStateMachine
{
    public bool isPatrol = false;
    public Transform target;
    public Animator eAnim;
    public float damage;
    public Transform enemy;
    public Transform[] waypoints;
    public float distance;
    public PlayerMovementSM playsm;
    public NavMeshAgent agent;
    [HideInInspector]
    public int destinations;
    public PlayerHealth health;
    public EnemyHealth eHealth;

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

    private void Awake()
    {
        idleState = new EnemyIdle(this);
        patrolState = new EnemyPatrol(this);
        chaseState = new EnemyChase(this);
        fireState = new EnemyShoot(this);
        meleeState = new EnemyMelee(this);
    }

    protected override EnemyBaseState GetInitialState()
    {
        return idleState;
    }
}
