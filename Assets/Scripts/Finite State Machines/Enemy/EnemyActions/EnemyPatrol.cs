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
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float IdleDist = 40;

        RaycastHit patrolHit;
        float rayLength = 20f;
        Ray patrolRay = new Ray(esm.FOV.transform.position, esm.FOV.transform.forward);

        if (DistToPlayer >= IdleDist)
        {
            enemyStateMachine.ChangeState(esm.idleState);
            esm.eAnim.SetBool("patrolling", false);
            AudioManager.manager.Stop("walk");
            esm.isPatrol = false;
        }

        if (Physics.Raycast(patrolRay, out patrolHit, rayLength) && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("chase", true);
            esm.isChasing = true;
            esm.isPatrol = false;
            AudioManager.manager.Play("sprinting");
            AudioManager.manager.Stop("walk");
            Debug.Log("CHASING PLAYER");
        }

        if (esm.playsm.weapon.gunEquipped && Physics.Raycast(patrolRay, out patrolHit, rayLength))
        {
            esm.eGun.gameObject.SetActive(true);
            enemyStateMachine.ChangeState(esm.fireState);
            esm.eAnim.SetBool("shoot", true);
            AudioManager.manager.Play("shootGun");
            AudioManager.manager.Stop("walk");
            esm.eAnim.SetTrigger("gunEquipped");
            esm.isPatrol = false;
            Debug.Log("FIRING GUN!");
            esm.isShooting = true;
        }

        if (esm.eHealth.health <= 65)
        {
            enemyStateMachine.ChangeState(esm.coverState);
            esm.eAnim.SetFloat("health", esm.eHealth.health);
            esm.isPatrol = false;
            esm.isHiding = true;
            Debug.Log("HIDING!");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (!esm.agent.pathPending && esm.agent.remainingDistance < 0.5 || esm.health.health == 0)
        {
            esm.GoToNextPoint();
        }
    }
}