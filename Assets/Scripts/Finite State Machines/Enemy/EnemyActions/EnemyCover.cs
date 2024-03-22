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

    public override void Enter()
    {
        base.Enter();

        esm.RandomIndexCheck();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (esm.agent.remainingDistance <= esm.agent.stoppingDistance)
        {
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("patrolling", false);
            esm.isPatrol = false;
            esm.agent.isStopped = true;
            Debug.Log("REACHED DESTINATION!");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.agent.SetDestination(esm.cols[esm.RandomIndex].transform.position);

    }

}
