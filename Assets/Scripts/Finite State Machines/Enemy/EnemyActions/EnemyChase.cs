using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyChase(EnemyMovementSM enemyStateMachine) : base("Chase", enemyStateMachine)
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

        // Is the player more than or equal to 20 metres away from the enemy?
        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) > 20 && !esm.playsm.weapon.gunEquipped || esm.health.health == 0)
        {
            // Enemy is patrolling
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("chase", false);
            esm.isPatrol = true;
            esm.isChasing = false;
        }

        // Is the enemy's health below or equal to 50 HP?
        if (esm.eHealth.health <= 65)
        {
            // Enemy is injured
            esm.eAnim.SetFloat("health", esm.eHealth.health);
            esm.isChasing = false;
            esm.isHiding = true;
            enemyStateMachine.ChangeState(esm.coverState);
        }

        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) <= 1.5f && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.meleeState);
            esm.isChasing = false;
            esm.isMeleeAttack = true;
            esm.eAnim.SetTrigger("punching");
            Debug.Log("PUNCHING PLAYER");
        }

        if (esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.fireState);
            esm.isChasing = false;
            esm.eGun.gameObject.SetActive(true);
            esm.isShooting = true;
            esm.eAnim.SetBool("shoot", true);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Agent moves to the player
        esm.agent.SetDestination(esm.target.position);

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}