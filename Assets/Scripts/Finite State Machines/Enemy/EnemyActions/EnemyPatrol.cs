using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        // Is the agent on a NavMesh?
        if (esm.agent.isOnNavMesh)
        {
            esm.agent.isStopped = false;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // If the health of the enemy below to or equal to 65HP?
        if (esm.eHealth.health <= 65)
        {
            esm.isPatrol = false;
            esm.isHiding = true;
            esm.eAnim.SetFloat("health", esm.eHealth.health);
            Debug.Log("HIDING!");

            enemyStateMachine.ChangeState(esm.coverState);
            return; // Halt logic tracking immediately to secure the state transition
        }

        // Is the player dead?
        if (esm.playerHealth.health <= 0)
        {
            // If the player is dead, drop engagement loops and remain strictly in patrol routing
            return;
        }

        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float IdleDist = 40f;

        // Is the distance to the player more than the idle distance range?
        if (DistToPlayer >= IdleDist)
        {
            esm.isPatrol = false;
            esm.eAnim.SetBool("patrolling", false);
            AudioManager.manager.Stop("walk");

            enemyStateMachine.ChangeState(esm.idleState);
            return;
        }

        // Setup vision scanning arrays targeting explicitly defined Player layers
        RaycastHit patrolHit;
        float rayLength = 20f;
        Ray patrolRay = new Ray(esm.FOV.transform.position, esm.FOV.transform.forward);

        // Combat Engagement Logic Checks
        if (Physics.Raycast(patrolRay, out patrolHit, rayLength, esm.Player))
        {
            // Does the player have their gun equipped?
            if (esm.playsm.weapon.gunEquipped)
            {
                esm.isPatrol = false;
                esm.isShooting = true;
                esm.eGun.gameObject.SetActive(true);
                esm.eAnim.SetBool("shoot", true);
                esm.eAnim.SetTrigger("gunEquipped");
                AudioManager.manager.Play("shootGun");
                AudioManager.manager.Stop("walk");
                Debug.Log("FIRING GUN!");

                enemyStateMachine.ChangeState(esm.fireState);
                return;
            }
            else
            {
                esm.isPatrol = false;
                esm.isChasing = true;
                esm.eAnim.SetBool("chase", true);
                AudioManager.manager.Play("sprinting");
                AudioManager.manager.Stop("walk");
                Debug.Log("CHASING PLAYER");

                enemyStateMachine.ChangeState(esm.chaseState);
                return;
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Safe path checks executed cleanly on fixed frame intervals
        if (esm.agent.isOnNavMesh && !esm.agent.pathPending)
        {
            if (esm.agent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }
    }

    private void GoToNextPoint()
    {
        // End of path checks
        if (esm.waypoints.Length == 0)
        {
            return;
        }

        esm.agent.destination = esm.waypoints[esm.destinations].position;
        esm.destinations = (esm.destinations + 1) % esm.waypoints.Length;
    }
}