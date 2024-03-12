using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyPatrol(EnemyMovementSM enemyStateMachine) : base("Patrol", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Vector3.Distance(esm.enemy.transform.position, esm.target.position) > 20)
        {
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("patrolling", false);
            esm.isPatrol = false;
        }

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) == 10)
        {
            Debug.Log("ATTACKING");
        }
    }
}
