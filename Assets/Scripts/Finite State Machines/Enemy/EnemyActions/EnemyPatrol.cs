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

        if (!esm.agent.pathPending && esm.agent.remainingDistance < 0.5)
        {
            GoToNextPoint();
        }

        if(Vector3.Distance(esm.enemy.transform.position, esm.target.position) > 40)
        {
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("patrolling", false);
            esm.isPatrol = false;
        }

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) <= 15)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("chase", true);
            esm.isPatrol = false;
            Debug.Log("CHASING PLAYER");
        }

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) <= 15 && esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.fireState);
            esm.eAnim.SetBool("shoot", true);
            esm.isPatrol = false;
            Debug.Log("ATTACKING");
        }

        void GoToNextPoint()
        {
            // End of path
            if (esm.waypoints.Length == 0)
            {
                return;
            }
            esm.agent.destination = esm.waypoints[esm.destinations].position;
            esm.destinations = (esm.destinations + 1) % esm.waypoints.Length;
        }
    }
}
