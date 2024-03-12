using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyIdle(EnemyMovementSM enemyStateMachine) : base("EnemyIdle", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Vector3.Distance(esm.enemy.transform.position, esm.target.position) <= 10)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.isPatrol = true;
        }
    }
}
