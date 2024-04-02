using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyBaseState
{
    EnemyMovementSM esm;
    float meleeDist = 1.5f;

    public EnemyMelee(EnemyMovementSM enemyStateMachine) : base("Melee", enemyStateMachine)
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

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) > meleeDist && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.isMeleeAttack = false;
            esm.isChasing = true;
            esm.eAnim.SetBool("chase", true);
        }

        if (esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.fireState);
            esm.isMeleeAttack = false;
            esm.eGun.gameObject.SetActive(true);
            esm.isShooting = true;
            esm.eAnim.SetBool("shoot", true);
        }

        if (esm.health.health == 0)
        {
            esm.GoToNextPoint();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (!esm.attackedPlayer)
        {
            esm.eMSystem.AttackPlayer();
        }

        esm.enemy.LookAt(esm.target);

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
