using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShoot : PoliceBaseState
{
    private PoliceMovementSM police;

    public PoliceShoot(PoliceMovementSM policeMachine) : base("Shoot", policeMachine)
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
        float DistToPlayer = Vector3.Distance(police.player.transform.position, police.PoliceAI.transform.position);

        /* if (police.health == 0)
         {
             esm.eHealth.StartCoroutine(esm.eHealth.Death());
         }
        */

        if (!police.playsm.weapon.gunEquipped && DistToPlayer >= police.policeGun.range || police.playsm.weapon.gunEquipped && DistToPlayer >= police.policeGun.range)
        {
            policeMachine.ChangeState(police.chaseState);
            police.PoliceAnim.SetBool("shoot", false);
            police.isChasing = true;
            police.isShooting = false;
            police.policeGun.gameObject.SetActive(false);
            police.PoliceAI.isStopped = false;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("sprinting");
        }

        if (police.playsm.health.health <= 0)
        {
            policeMachine.ChangeState(police.patrolState);
            police.PoliceAnim.SetBool("playerDead", true);
            police.isShooting = false;
            police.isPatrolling = true;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        police.officer.LookAt(police.player.transform.position);

        // Finds the distance between the enemy and the player
        Vector3 direction = police.player.transform.position - police.officer.transform.position;

        // Turns the enemy to face towards the player.
        police.officer.transform.rotation = Quaternion.Slerp(police.officer.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
