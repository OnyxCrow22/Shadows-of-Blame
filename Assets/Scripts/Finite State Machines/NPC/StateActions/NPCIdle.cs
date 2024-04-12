using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCIdle : NPCBaseState
{
    private NPCMovementSM AI;

    public NPCIdle(NPCMovementSM npcStateMachine) : base("NPCIdle", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void Enter()
    {
        
    }

    public override void UpdateLogic()
    {
        if (Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) >= 0.5f)
        {
            npcStateMachine.ChangeState(AI.walkingState);
            AI.NPCAnim.SetBool("walking", true);
            AI.isWalking = true;
        }

        if (Vector3.Distance(AI.playsm.player.position, AI.NPC.transform.position) > 70)
        {
            AI.NPC.AddComponent<RemoveNPC>().OnBecameInvisible();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
