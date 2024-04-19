using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : NPCBaseState
{
    private NPCMovementSM AI;
    float WalkDist = 0.5f;

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
        Ray gunRay = new Ray(AI.NPCFOV.transform.position, Vector3.forward);
        RaycastHit gunHit;
        float radius = 20;

        if (Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) < WalkDist)
        {
            npcStateMachine.ChangeState(AI.idleState);
            AI.NPCAnim.SetBool("walking", false);
        }

        // Player is crazy, run away!
        if (Physics.Raycast(gunRay, out gunHit, radius) && AI.playsm.isShooting)
        {
            AI.StartCoroutine(AI.ScreamFlee());

            AI.SearchNPCS();
        }

        if (AI.nHealth.health <= 0)
        {
            AI.nHealth.StartCoroutine(AI.nHealth.NPCDeath());
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
