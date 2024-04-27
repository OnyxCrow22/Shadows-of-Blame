using Unity.VisualScripting;
using UnityEngine;

public class PoliceChase : PoliceBaseState
{
    private PoliceMovementSM police;

    public PoliceChase(PoliceMovementSM policeMachine) : base("Chase", policeMachine)
    {
        police = policeMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Ray punchRay = new Ray(police.PoliceFOV.transform.position, Vector3.forward);
        RaycastHit punchHit;
        float punchLength = 2.5f;

        // Is the player more than or equal to 20 metres away from the enemy?
        if (PoliceLevel.policeLevels == 0 || police.playsm.health.health == 0)
        {
            // Go back on patrol.
            policeMachine.ChangeState(police.patrolState);
            police.PoliceAnim.SetBool("chase", false);
        }

     /*   // Is the enemy's health below or equal to 50 HP?
        if (police.pHealth.health <= 65)
        {
            // Enemy is injured
            esm.eAnim.SetFloat("health", esm.eHealth.health);
            esm.isChasing = false;
            esm.isHiding = true;
            enemyStateMachine.ChangeState(esm.coverState);
        }
     
        if (Physics.Raycast(punchRay, out punchHit, punchLength) && !police.playsm.weapon.gunEquipped)
        {
            policeMachine.ChangeState(police.meleeState);
            police.PoliceAnim.SetTrigger("punching");
            AudioManager.manager.Play("punch");
            AudioManager.manager.Stop("sprinting");
            Debug.Log("PUNCHING PLAYER");
        }
     

        if (police.playsm.weapon.gunEquipped)
        {
            policeMachine.ChangeState(police.fireState);
            police.pGun.gameObject.SetActive(true);
            police.PoliceAnim.SetBool("shoot", true);
            AudioManager.manager.Stop("sprinting");
            AudioManager.manager.Play("shootGun");
        }

    */

        if (police.playsm.health.health == 0)
        {
            policeMachine.ChangeState(police.patrolState);
            police.playsm.isPlayerDead = true;
            police.PoliceAnim.SetBool("walking", true);
            AudioManager.manager.Play("walk");
            AudioManager.manager.Stop("sprinting");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Agent moves to the player
        police.PoliceAI.SetDestination(police.playsm.player.position);

        // Finds the distance between the enemy and the player
        Vector3 direction = police.playsm.player.position - police.PoliceAI.transform.position;

        // Turns the enemy to face towards the player.
        police.PoliceAI.transform.rotation = Quaternion.Slerp(police.PoliceAI.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}