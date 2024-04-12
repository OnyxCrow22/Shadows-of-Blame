using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCFlee : NPCBaseState
{
    private NPCMovementSM AI;
    float FleeDist = 64;

    public NPCFlee(NPCMovementSM npcStateMachine) : base("NPCWalk", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // Okay, we can calm down now, as the player is no longer holding a gun.
        if (!AI.playsm.weapon.gunEquipped && FleeDist >= 64)
        {
            npcStateMachine.ChangeState(AI.walkingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        if (AI.playsm.weapon.gunEquipped)
        {
            Vector3 runDist = AI.NPC.transform.position - AI.player.transform.position;

            Vector3 newPosition = AI.NPC.transform.position + runDist;

            AI.NPC.SetDestination(newPosition);
        }
    }
}
