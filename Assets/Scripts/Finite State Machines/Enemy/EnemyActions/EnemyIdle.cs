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

        if(Vector3.Distance(esm.target.position, esm.enemy.transform.position) > 20)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.isPatrol = true;
        }
    }
}
