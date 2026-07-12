using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : NPCBaseState
{
    private NPCMovementSM AI;
    float WalkDist = 120f;

    public NPCWalk(NPCMovementSM npcStateMachine) : base("NPCWalk", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float distanceFromPlayer = Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position);
        Ray gunRay = new Ray(AI.NPCFOV.transform.position, AI.NPCFOV.transform.forward);
        RaycastHit gunHit;
        float radius = 20;

        if (distanceFromPlayer > WalkDist)
        {
            AI.NPCAnim.SetBool("walking", false);
            npcStateMachine.ChangeState(AI.idleState);
            return;
        }

        // Is the civilian dead?
        if (AI.nHealth.health <= 0)
        {
            AI.nHealth.StartCoroutine(AI.nHealth.NPCDeath());
            return;
        }

        // Threat Detection Check
        if ((Physics.Raycast(gunRay, out gunHit, radius) && AI.playsm.isShooting) ||
            (Physics.Raycast(gunRay, out gunHit, radius) && AI.playsm.throwingGrenade) ||
            AI.playsm.isShooting)
        {
            // Is the civilian not fleeing or shooting?
            if (!AI.isFleeing && !AI.isShooting)
            {
                AI.aggression = (Random.value < 0.20f) ? 1 : 0; // Have a 20% chance of turning aggressive, and an 80% of fleeing.
            }

            // NPC flees from the player
            if (AI.aggression == 0)
            {
                AI.neturalNPC = true;
                AI.isWalking = false;
                AI.isFleeing = true;

                AI.SearchNPCS();
                AI.StartCoroutine(AI.ScreamFlee());
                AI.StartCoroutine(AI.ReturnDelay());

                AI.NPC.speed = Random.Range(4, 7);

                npcStateMachine.ChangeState(AI.fleeState);
                return;
            }
            // NPC decides to fight back against the player
            else if (AI.aggression == 1)
            {
                AI.isWalking = false;
                AI.isShooting = true;
                AI.hostileNPC = true;
                AI.hiddenGun.SetActive(true);
                AI.NPCAnim.SetBool("shoot", true);
                AI.NPCAnim.SetTrigger("gunEquipped");

                AudioManager.manager.Play("shoot");
                AudioManager.manager.Stop("walk");

                npcStateMachine.ChangeState(AI.fireState);
                return;
            }
        }
    }
}