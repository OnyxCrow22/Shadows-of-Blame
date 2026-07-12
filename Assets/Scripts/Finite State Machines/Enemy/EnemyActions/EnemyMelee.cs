using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyBaseState
{
    EnemyMovementSM esm;
    private bool canAttack = false;

    public EnemyMelee(EnemyMovementSM enemyStateMachine) : base("Melee", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        // Is the enemy on a NavMesh object?
        if (esm.agent.isOnNavMesh)
        {
            esm.agent.isStopped = true; // Stop them.
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (canAttack)
        {
            esm.eMSystem.PerformAttack(esm.damage, esm.eHealth);
            esm.StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(esm.attackDelay);
        canAttack = true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Track rotation direction targeting user positions
        Vector3 direction = esm.target.position - esm.enemy.transform.position;
        direction.y = 0f; // Constrain vertical pivoting loops to eliminate structural tipping

        if (direction != Vector3.zero)
        {
            esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}