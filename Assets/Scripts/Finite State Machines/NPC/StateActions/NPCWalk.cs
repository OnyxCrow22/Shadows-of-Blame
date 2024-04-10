using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        float distanceFromPlayer = Vector3.Distance(AI.NPC.transform.position, AI.playsm.transform.position);

        if (distanceFromPlayer < 70)
        {
            npcStateMachine.ChangeState(AI.idleState);
            AI.NPCAnim.SetBool("walking", false);
        }

        // Player is crazy, run away!
        if (AI.playsm.weapon.gunEquipped)
        {
          //  npcStateMachine.ChangeState(AI.fleeState);
            AI.NPCAnim.SetBool("terrified", true);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
