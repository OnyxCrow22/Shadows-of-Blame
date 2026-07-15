using UnityEngine;

public class PolicePatrol : PoliceBaseState
{
    private PoliceMovementSM wanted;

    public PolicePatrol(PoliceMovementSM policeMachine) : base("PolicePatrol", policeMachine)
    {
        wanted = policeMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Scan for threats nearby
        Ray gunRay = new Ray(wanted.PoliceFOV.transform.position, wanted.PoliceFOV.transform.forward);
        float distToPlayer = Vector3.Distance(wanted.player.transform.position, wanted.PoliceAI.transform.position);

        // Shoot the player if the wanted level goes above 2, or if the player has a gun equipped, or is throwing grenades, or is shooting.
        if (PoliceLevel.policeLevels >= 2 || (Physics.Raycast(gunRay, 20f))) // (wanted.playsm.weapon.gunEquipped || wanted.playsm.throwingGrenade || wanted.playsm.isShooting)))
        {
            wanted.isPatrolling = false;
            wanted.isShooting = true;
            // wanted.policeGun.policeGun.SetActive(true);
            wanted.PoliceAnim.SetBool("shoot", true);
            policeMachine.ChangeState(wanted.fireState);
            return;
        }

        // Chase after the player with the goal of arresting them.
        if (PoliceLevel.policeLevels >= 1)
        {
            wanted.PoliceAnim.SetBool("chase", true);
            policeMachine.ChangeState(wanted.chaseState);
            return;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (wanted.PoliceAI.isOnNavMesh)
            wanted.PoliceAI.SetDestination(wanted.follow.currentPedestrianNode.transform.position);
    }
}