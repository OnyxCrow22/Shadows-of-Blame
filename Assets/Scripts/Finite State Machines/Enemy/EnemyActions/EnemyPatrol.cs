using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float IdleDist = 40;

        RaycastHit patrolHit;
        float rayLength = 10f;
        Ray patrolRay = new Ray(esm.FOV.transform.position, Vector3.forward);

        if (!esm.agent.pathPending && esm.agent.remainingDistance < 0.5)
        {
            GoToNextPoint();
        }

        if (DistToPlayer >= IdleDist)
        {
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("patrolling", false);
            esm.isPatrol = false;
        }

        if (!esm.playsm.weapon.gunEquipped && Physics.Raycast(patrolRay, out patrolHit, rayLength))
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("chase", true);
            esm.isPatrol = false;
            Debug.Log("CHASING PLAYER");
        }

        if (esm.playsm.weapon.gunEquipped && Physics.Raycast(patrolRay, out patrolHit, rayLength))
        {
            esm.eGun.gameObject.SetActive(true);
            enemyStateMachine.ChangeState(esm.fireState);
            esm.eAnim.SetBool("shoot", true);
            esm.isPatrol = false;
            Debug.Log("FIRING GUN!");
            esm.shoot = true;
        }

        if (esm.eHealth.health <= 65)
        {
            enemyStateMachine.ChangeState(esm.coverState);
            Debug.Log("HIDING!");
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