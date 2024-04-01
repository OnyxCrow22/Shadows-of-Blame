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

        RaycastHit chaseHit;
        float rayLength = 10f;
        Ray chaseRay = new Ray(esm.FOV.transform.position, Vector3.forward);

        // Is the player more than or equal to 20 metres away from the enemy?
        if (!Physics.Raycast(chaseRay, out chaseHit, rayLength) && !esm.playsm.weapon.gunEquipped)
        {
            // Enemy is patrolling
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.isPatrol = true;
            esm.isChasing = false;
            esm.agent.isStopped = false;
        }

        // Is the enemy's health below or equal to 50 HP?
        if (esm.eHealth.health <= 65)
        {
            // Enemy is injured
            esm.eAnim.SetBool("injuredRun", true);
            esm.isChasing = false;
            esm.isHiding = true;
            enemyStateMachine.ChangeState(esm.coverState);
        }

        if (Physics.Raycast(chaseRay, out chaseHit, rayLength) && !esm.playsm.weapon.gunEquipped)
        {
            enemyStateMachine.ChangeState(esm.meleeState);
            esm.isChasing = false;
            esm.isMeleeAttack = true;
            esm.eAnim.SetBool("punching", true);
            esm.eAnim.SetBool("chase", false);
            Debug.Log("PUNCHING PLAYER");
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