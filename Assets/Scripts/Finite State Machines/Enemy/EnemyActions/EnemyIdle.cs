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
        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float PatrolDist = 20;

        base.UpdateLogic();

        if(DistToPlayer <= PatrolDist && !esm.isHiding)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            AudioManager.manager.Play("walk");
            esm.isPatrol = true;
        }
    }
}
