using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlee : NPCBaseState
{
    private NPCMovementSM AI;

    public NPCFlee(NPCMovementSM npcStateMachine) : base("NPCWalk", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float currentDistanceToPlayer = Vector3.Distance(AI.NPC.transform.position, AI.player.transform.position);

        // Okay, we can calm down now, if the player isn't actively threatening and is far away
        if (AI.canReturn && currentDistanceToPlayer >= 64f)
        {
            if (!AI.playsm.weapon.gunEquipped || !AI.playsm.hasThrownGrenade)
            {
                AI.isFleeing = false;
                AI.isWalking = true;
                AI.NPCAnim.SetBool("walking", true);
                AI.NPCAnim.SetBool("flee", false);

                int RandomSpeedIndex = Random.Range(1, 3);
                AI.NPC.speed = RandomSpeedIndex;

                npcStateMachine.ChangeState(AI.walkingState);
                return;
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}