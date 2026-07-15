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

        // Is the officer dead?
        if (police.pHealth.health <= 0) // { police.pHealth.StartCoroutine(police.pHealth.PoliceDeath()); return; }

        // Return to chase sequence if the player is out of range
        if (DistToPlayer >= police.policeGun.range)
        {
            police.PoliceAnim.SetBool("shoot", false);
            police.policeGun.gameObject.SetActive(false);
            police.isShooting = false;
            police.isChasing = true;
            police.PoliceAI.isStopped = false;
            policeMachine.ChangeState(police.chaseState);
            return;
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
