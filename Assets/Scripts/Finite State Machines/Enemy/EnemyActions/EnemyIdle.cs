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
        base.Enter();

        // Stop the NavMesh agent from drifting or sliding around while idling
        if (esm.agent.isOnNavMesh)
        {
            esm.agent.isStopped = true;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float PatrolDist = 20f;

        // Is the Player in range, which is less than the patrol distance, and they are not hiding?
        if (DistToPlayer <= PatrolDist && !esm.isHiding)
        {
            esm.isPatrol = true;
            esm.eAnim.SetBool("patrolling", true);
            AudioManager.manager.Play("walk");

            enemyStateMachine.ChangeState(esm.patrolState);
            return;
        }
    }
}