using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCover : EnemyBaseState
{
    EnemyMovementSM esm;

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
            Debug.Log("REACHED DESTINATION!");
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("chase", false);
            esm.eAnim.SetBool("hiding", true);
            esm.agent.isStopped = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.agent.SetDestination(esm.cols[esm.RandomIndex].transform.position);
    }
}
