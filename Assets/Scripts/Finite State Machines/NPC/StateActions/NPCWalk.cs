using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : NPCBaseState
{
    private NPCMovementSM AI;
    float WalkDist = 40;

    public NPCWalk(NPCMovementSM npcStateMachine) : base("NPCWalk", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        float distanceFromPlayer = Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position);

        if (Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) < WalkDist)
        {
            npcStateMachine.ChangeState(AI.idleState);
            AI.NPCAnim.SetBool("walking", false);
        }

        // Player is crazy, run away!
        if (AI.playsm.weapon.gunEquipped)
        {
          //  npcStateMachine.ChangeState(AI.fleeState);
            AI.NPCAnim.SetBool("flee", true);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
