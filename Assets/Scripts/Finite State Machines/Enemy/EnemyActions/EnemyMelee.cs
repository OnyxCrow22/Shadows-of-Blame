using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyBaseState
{
    EnemyMovementSM esm;

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
        Ray meleeRay = new Ray(esm.FOV.transform.position, Vector3.forward);
        RaycastHit meleeHit;
        float meleeLength = 2.5f;

        if (Physics.Raycast(meleeRay, out meleeHit, meleeLength) && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.isMeleeAttack = false;
            esm.isChasing = true;
            esm.eAnim.SetBool("chase", true);
            AudioManager.manager.Stop("punch");
            AudioManager.manager.Play("sprinting");
        }

        if (esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.fireState);
            esm.isMeleeAttack = false;
            esm.eGun.gameObject.SetActive(true);
            esm.isShooting = true;
            esm.eAnim.SetBool("shoot", true);
            AudioManager.manager.Stop("punch");
            AudioManager.manager.Play("shootGun");
        }

        if (esm.health.health == 0)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.isMeleeAttack = false;
            esm.playsm.isPlayerDead = true;
            esm.eAnim.ResetTrigger("punching");
            AudioManager.manager.Stop("punch");
            AudioManager.manager.Play("walk");
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

        if (esm.health.health == 0)
        {
            GoToNextPoint();
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

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
