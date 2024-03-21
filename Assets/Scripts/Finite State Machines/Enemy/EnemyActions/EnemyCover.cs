using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCover : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyCover(EnemyMovementSM enemyStateMachine) : base("Cover", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public void Awake()
    {
        esm.RandomIndex = Random.Range(0, esm.cols.Length);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.agent.SetDestination(esm.cols[esm.RandomIndex].transform.position);

        if(esm.agent.remainingDistance == 0)
        {
            esm.agent.isStopped = true;
        }

    }

}
