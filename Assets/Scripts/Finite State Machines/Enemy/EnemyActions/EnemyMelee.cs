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

        if (Vector3.Distance(esm.target.position, esm.enemy.transform.position) > 5 && !esm.playsm.weapon.gunEquipped && esm.attacking)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("punching", false);
            esm.attacking = false;
            esm.dealDamage = false;
        }

        if (esm.attacking)
        {
            esm.health.LoseHealth();
            esm.eHealth.ResetAttack();
        }

        if (esm.health.health == 0 && esm.health.maxHealth == 0 && esm.attacking)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("punching", false);
            esm.attacking = false;
            esm.dealDamage = false;
            esm.isPatrol = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (esm.health.health == 0 && esm.health.maxHealth == 0 && esm.attacking)
        {
            esm.agent.SetDestination(esm.ePoint.transform.position);
        }
    }
}
