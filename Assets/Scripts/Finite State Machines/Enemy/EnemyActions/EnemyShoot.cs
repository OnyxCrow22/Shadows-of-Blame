using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyShoot(EnemyMovementSM enemyStateMachine) : base("Shoot", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        // Is the enemy on a NavMesh?
        if (esm.agent.isOnNavMesh)
        {
            esm.agent.isStopped = true;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Is the enemy dead?
        if (esm.eHealth.health == 0)
        {
            esm.eHealth.TakeDamage(esm.eHealth.maxHealth);
            return; // Instantly halts execution loops
        }

        // Is the enemy's health below to or equal to 65HP?
        if (esm.eHealth.health <= 65)
        {
            esm.isShooting = false;
            esm.isHiding = true;
            esm.eAnim.SetBool("shoot", false);
            esm.eGun.gameObject.SetActive(false);
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");
            Debug.Log("HIDING!");

            enemyStateMachine.ChangeState(esm.coverState);
            return;
        }

        // Is the player dead?
        if (esm.playerHealth.health <= 0)
        {
            esm.isShooting = false;
            esm.isPatrol = true;
            esm.eAnim.SetBool("playerDead", true);
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");

            enemyStateMachine.ChangeState(esm.patrolState);
            return;
        }

        // Combat Range Target Evaluation Check
        float DistToPlayer = Vector3.Distance(esm.enemy.transform.position, esm.target.position);

        // Is the distance to the player more than or equal to the Gun range?
        if (DistToPlayer >= esm.eGun.range)
        {
            esm.isChasing = true;
            esm.isShooting = false;
            esm.eAnim.SetBool("shoot", false);
            esm.eGun.gameObject.SetActive(false);

            // Is the enemy on a NavMesh?
            if (esm.agent.isOnNavMesh)
            {
                esm.agent.isStopped = false; // Restore movement capabilities for the chase
            }

            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("sprinting");

            enemyStateMachine.ChangeState(esm.chaseState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Target orientation tracking vectors locked strictly to the horizontal plane
        Vector3 direction = esm.target.position - esm.enemy.transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}