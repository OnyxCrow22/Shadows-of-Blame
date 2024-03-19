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

        // Is the player NOT in the FOV view?
        if (esm.eFOV.alertLevel == 0)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("patrolling", true);
            esm.isPatrol = true;
            esm.attacking = false;
        }

        if (esm.eHealth.health <= 50)
        {
            esm.eAnim.SetBool("injuredRun", true);
            enemyStateMachine.ChangeState(esm.coverState);
        }

        if (esm.eFOV.alertLevel == 100 && !esm.playsm.weapon.gunEquipped && !esm.attacking)
        {
            enemyStateMachine.ChangeState(esm.meleeState);
            esm.eAnim.SetBool("punching", true);
            esm.attacking = true;
            esm.dealDamage = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.agent.SetDestination(esm.target.position);

        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}