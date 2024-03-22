using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBaseState
{
    private EnemyMovementSM esm;
    

    public EnemyMelee(EnemyMovementSM enemyStateMachine) : base("Punch", enemyStateMachine)
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

        float distToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float ChaseDist = 10;
        float PatrolDist = 20;

        if (distToPlayer >= ChaseDist && !esm.playsm.weapon.gunEquipped && esm.attacking)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("chase", true);
            esm.attacking = false;
            esm.dealDamage = false;
            esm.agent.isStopped = false;
        }

        if (distToPlayer >= PatrolDist && esm.attacking)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.agent.isStopped = false;
            esm.attacking = false;
            esm.dealDamage = false;
        }

        if (esm.attacking)
        {
            esm.health.LoseHealth(esm.health.healthLoss);
            esm.eHealth.ResetAttack();
        }

        // Is the enemy's health below or equal to 65 HP?
        if (esm.eHealth.health <= 65)
        {
            enemyStateMachine.ChangeState(esm.coverState);
            Debug.Log("DIVING INTO COVER!");
        }

        // Player dead
        if (esm.health.health == 0 && esm.health.maxHealth == 0)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.agent.isStopped = false;
            esm.attacking = false;
            esm.dealDamage = false;
            esm.isPatrol = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        //Move the enemy away from the player.
        if (esm.health.health == 0 && esm.health.maxHealth == 0 && esm.attacking)
        {
            esm.agent.SetDestination(esm.ePoint.transform.position);
        }
    }
}
