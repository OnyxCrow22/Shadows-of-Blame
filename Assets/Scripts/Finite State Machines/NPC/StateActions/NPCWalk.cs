using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : NPCBaseState
{
    private NPCMovementSM AI;
    float WalkDist = 0.5f;
    GameObject[] male;
    GameObject[] female;

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
            npcStateMachine.ChangeState(AI.fleeState);
            AI.isFleeing = true;
            AI.isWalking = false;
            AI.NPCAnim.SetBool("flee", true);

            female = GameObject.FindGameObjectsWithTag("FemaleNPC");
            male = GameObject.FindGameObjectsWithTag("MaleNPC");

            if (female == GameObject.FindGameObjectsWithTag("FemaleNPC"))
            {
                AudioManager.manager.Play("femaleScream");
            }
            else if (male == GameObject.FindGameObjectsWithTag("MaleNPC"))
            {
                AudioManager.manager.Play("maleScream");
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
