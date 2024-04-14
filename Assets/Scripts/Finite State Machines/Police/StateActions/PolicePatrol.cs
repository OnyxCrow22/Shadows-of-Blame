using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PolicePatrol : PoliceBaseState
{
    private PoliceMovementSM wanted;
    float WalkDist = 0.5f;

    public PolicePatrol(PoliceMovementSM policeMachine) : base("PolicePatrol", policeMachine)
    {
        wanted = policeMachine;
    }

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        float distanceFromPlayer = Vector3.Distance(wanted.player.transform.position, wanted.PoliceAI.transform.position);
        Ray gunRay = new Ray(wanted.PoliceFOV.transform.position, Vector3.forward);
        RaycastHit gunHit;
        float radius = 20;

        if (Vector3.Distance(wanted.player.transform.position, wanted.PoliceAI.transform.position) < WalkDist)
        {
            policeMachine.ChangeState(wanted.idleState);
            wanted.PoliceAnim.SetBool("walking", false);
        }

        if (PoliceLevel.levelStage == 1)
        {
            policeMachine.ChangeState(wanted.chaseState);
            wanted.PoliceAnim.SetBool("chase", true);
        }

        // Player is crazy, shoot them!
        if (Physics.Raycast(gunRay, out gunHit, radius) && wanted.playsm.weapon.gunEquipped || PoliceLevel.levelStage >= 2)
        {
           // policeMachine.ChangeState(wanted.fireState);
            wanted.isPatrolling = false;
            wanted.isShooting = true;
            wanted.PoliceAnim.SetBool("shoot", true);
            AudioManager.manager.Play("shootGun");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
