using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : NPCBaseState
{
    private NPCMovementSM AI;

    public NPCWalk(NPCMovementSM npcStateMachine) : base("NPCWalk", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void Enter()
    {
        
    }

    public override void UpdateLogic()
    {
        float distanceFromPlayer = Vector3.Distance(AI.NPC.transform.position, AI.playsm.player.transform.position);

        if (distanceFromPlayer > 70)
        {
            AI.removed.DestroyNPC();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
