using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : NPCBaseState
{
    private NPCMovementSM npc;

    public NPCIdle(NPCMovementSM npcStateMachine) : base("NPCIdle", npcStateMachine)
    {
        npc = npcStateMachine;
    }

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        float distanceFromPlayer = Vector3.Distance(npc.NPC.transform.position, npc.playsm.player.transform.position);

        if (distanceFromPlayer == 60)
        {
            npcStateMachine.ChangeState(npc.walkingState);
        }
    }
}
