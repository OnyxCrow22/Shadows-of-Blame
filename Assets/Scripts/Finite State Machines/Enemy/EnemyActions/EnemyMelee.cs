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

        if (Vector3.Distance(esm.target.position, esm.enemy.transform.position) > 5 && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("punching", false);
        }

        esm.playsm.health.LoseHealth(esm.damage);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
