using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : NPCBaseState
{
    private NPCMovementSM AI;

    public NPCIdle(NPCMovementSM npcStateMachine) : base("NPCIdle", npcStateMachine)
    {
        AI = npcStateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float DistToPlayer = Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position);
        Ray gunRay = new Ray(AI.NPCFOV.transform.position, AI.NPCFOV.transform.forward);
        float threatRadius = 20f;

        if (AI.playsm.isShooting || AI.playsm.throwingGrenade || AI.playsm.weapon.gunEquipped)
        {
            if (DistToPlayer <= threatRadius)
            {
                // Roll personality check on frame one of the threat detection
                if (!AI.isFleeing && !AI.isShooting)
                {
                    AI.aggression = (Random.value < 0.20f) ? 1 : 0;
                }

                // NPC panics and flees from the player
                if (AI.aggression == 0)
                {
                    AI.neturalNPC = true;
                    AI.isWalking = false;
                    AI.isFleeing = true;

                    AI.SearchNPCS();
                    AI.StartCoroutine(AI.ScreamFlee());
                    AI.StartCoroutine(AI.ReturnDelay());

                    AI.NPC.speed = Random.Range(4, 7);
                    AI.NPC.isStopped = false;

                    npcStateMachine.ChangeState(AI.fleeState);
                    return;
                }
                // NPC fights back against the player
                else if (AI.aggression == 1)
                {
                    AI.isWalking = false;
                    AI.isShooting = true;
                    AI.hostileNPC = true;
                    AI.hiddenGun.SetActive(true);
                    AI.NPCAnim.SetBool("shoot", true);
                    AI.NPCAnim.SetTrigger("gunEquipped");

                    AudioManager.manager.Play("shoot");
                    AI.NPC.isStopped = true;

                    npcStateMachine.ChangeState(AI.fireState);
                    return;
                }
            }
        }

        // Normal, peaceful proximity check
        if (DistToPlayer >= 0.5f && DistToPlayer < 120f)
        {
            AI.NPCAnim.SetBool("walking", true);
            AI.NPCSound.PlayOneShot(AI.clips[2]);
            AI.isWalking = true;

            if (AI.NPC.isOnNavMesh)
            {
                AI.NPC.isStopped = false;
            }

            npcStateMachine.ChangeState(AI.walkingState);
            return;
        }
    }
}