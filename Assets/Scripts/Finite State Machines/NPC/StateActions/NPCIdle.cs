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
        if (Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) >= 0.5f && Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) < 120)
        {
            npcStateMachine.ChangeState(AI.walkingState);
            AI.NPCAnim.SetBool("walking", true);
            AI.NPCSound.PlayOneShot(AI.clips[2]);
            AI.isWalking = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
