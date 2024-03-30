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
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float DistToPlayer = Vector3.Distance(esm.target.position, esm.enemy.transform.position);
        float ChaseDist = 5;

        if (esm.eHealth.health <= 65)
        {
            esm.ecMaster.HandleGainSight(esm.enemy);
            Debug.Log("HIDING!");
            esm.eAnim.SetBool("shoot", false);
            esm.shoot = false;
            esm.attacking = false;
        }

        if (DistToPlayer >= ChaseDist)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("chase", true);
            esm.attacking = false;
            esm.shoot = false;
            esm.eGun.gameObject.SetActive(false);
            esm.agent.isStopped = false;
        }
    }



    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
