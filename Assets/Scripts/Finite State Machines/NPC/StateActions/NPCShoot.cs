using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShoot : NPCBaseState
{
    private NPCMovementSM AI;

    public NPCShoot(NPCMovementSM npcStateMachine) : base("Shoot", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float DistToPlayer = Vector3.Distance(AI.NPC.transform.position, AI.player.transform.position);

        if (AI.nHealth.health <= 0)
        {
            AI.nHealth.StartCoroutine(AI.nHealth.NPCDeath());
        }

        if (!AI.playsm.weapon.gunEquipped && DistToPlayer >= AI.hidden.range || AI.playsm.weapon.gunEquipped && DistToPlayer >= AI.hidden.range)
        {
            npcStateMachine.ChangeState(AI.walkingState);
            AI.NPCAnim.SetBool("shoot", false);
            AI.isWalking = true;
            AI.isShooting = false;
            AI.hidden.gameObject.SetActive(false);
            AI.NPC.isStopped = false;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("sprinting");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        AI.NPC.transform.LookAt(AI.player.transform.position);

        // Finds the distance between the enemy and the player
        Vector3 direction = AI.player.transform.position - AI.NPC.transform.position;

        // Turns the enemy to face towards the player.
        AI.NPC.transform.rotation = Quaternion.Slerp(AI.NPC.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
