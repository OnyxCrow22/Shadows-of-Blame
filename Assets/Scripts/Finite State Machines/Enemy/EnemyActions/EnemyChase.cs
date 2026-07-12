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
        Ray punchRay = new Ray(esm.FOV.transform.position, esm.FOV.transform.forward);
        RaycastHit punchHit;
        float punchLength = 2.5f;

        // Is the enemy's health below or equal to 65 HP?
        if (esm.eHealth.health <= 65)
        {
            esm.eAnim.SetFloat("health", esm.eHealth.health);
            esm.isChasing = false;
            esm.isHiding = true;
            enemyStateMachine.ChangeState(esm.coverState);
            return; // Stops execution immediately so states below don't overwrite this
        }

        // Is the player dead?
        if (esm.playerHealth.health <= 0)
        {
            esm.isChasing = false;
            esm.playsm.isPlayerDead = true;
            esm.eAnim.SetBool("patrolling", true);
            AudioManager.manager.Play("walk");
            AudioManager.manager.Stop("sprinting");
            enemyStateMachine.ChangeState(esm.patrolState);
            return;
        }

        // Is the player's weapon equipped?
        if (esm.playsm.weapon.gunEquipped)
        {
            esm.isChasing = false;
            esm.eGun.gameObject.SetActive(true);
            esm.isShooting = true;
            esm.eAnim.SetBool("shoot", true);
            AudioManager.manager.Stop("sprinting");
            AudioManager.manager.Play("shootGun");
            enemyStateMachine.ChangeState(esm.fireState);
            return;
        }

        // Can the enemy punch the player?
        if (Physics.Raycast(punchRay, out punchHit, punchLength, esm.Player))
        {
            enemyStateMachine.ChangeState(esm.meleeState);
            esm.isChasing = false;
            esm.isMeleeAttack = true;
            esm.eAnim.SetTrigger("punching");
            AudioManager.manager.Play("punch");
            AudioManager.manager.Stop("sprinting");
            Debug.Log("PUNCHING PLAYER");
            return;
        }

        // Is the player more than 20 meters away?
        if (Vector3.Distance(esm.enemy.transform.position, esm.target.position) > 20)
        {
            esm.eAnim.SetBool("chase", false);
            esm.isPatrol = true;
            esm.isChasing = false;
            enemyStateMachine.ChangeState(esm.patrolState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Agent moves to the player safely if on the NavMesh
        if (esm.agent.isOnNavMesh)
        {
            esm.agent.SetDestination(esm.target.position);
        }

        // Finds the direction between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;
        direction.y = 0; // Lock the Y axis to keep the enemy standing straight up while turning

        // Turns the enemy to face towards the player smoothly
        if (direction != Vector3.zero)
        {
            esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}