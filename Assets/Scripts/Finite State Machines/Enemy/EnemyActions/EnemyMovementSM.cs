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
    public NavMeshAgent agent;
    [HideInInspector]
    public int destinations;

    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public EnemyPatrol patrolState;

    private void Awake()
    {
        idleState = new EnemyIdle(this);
        patrolState = new EnemyPatrol(this);
    }

    protected override EnemyBaseState GetInitialState()
    {
        return idleState;
    }
}
