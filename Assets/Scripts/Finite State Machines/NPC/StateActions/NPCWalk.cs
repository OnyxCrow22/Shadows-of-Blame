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

    public override void Enter()
    {

    }

    public override void UpdateLogic()
    {
        float distanceFromPlayer = Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position);
        Ray gunRay = new Ray(AI.NPCFOV.transform.position, Vector3.forward);
        RaycastHit gunHit;
        float radius = 20;

        if (Vector3.Distance(AI.player.transform.position, AI.NPC.transform.position) > WalkDist)
        {
            npcStateMachine.ChangeState(AI.idleState);
            AI.NPCAnim.SetBool("walking", false);
        }

        // Player is crazy, run away!
        if (Physics.Raycast(gunRay, out gunHit, radius) && AI.playsm.isShooting || (Physics.Raycast(gunRay, out gunHit, radius) && AI.playsm.throwingGrenade || AI.playsm.isShooting))
        {
            // NPC is a female, they aren't aggressive.
            if (AI.isFemale)
            {
                AI.aggression = 0;
            }
            // NPC is not aggressive, they need to run away!
            if (AI.aggression == 0)
            {
                AI.StartCoroutine(AI.ScreamFlee());
                AI.neturalNPC = true;
                AI.isWalking = false;
                AI.isFleeing = true;

                AI.SearchNPCS();

                if (AI.isFleeing)
                {
                    AI.StartCoroutine(AI.ReturnDelay());
                    int RandomSpeedIndex = Random.Range(4, 7);
                    AI.NPC.speed = RandomSpeedIndex;
                }
            }
            // NPC is aggressive, they will not run away easily.
            else if (AI.aggression == 1 && AI.isMale)
            {
                npcStateMachine.ChangeState(AI.fireState);
                AI.hiddenGun.SetActive(true);
                AI.NPCAnim.SetBool("shoot", true);
                AudioManager.manager.Play("shoot");
                AudioManager.manager.Stop("walk");
                AI.NPCAnim.SetTrigger("gunEquipped");
                AI.isWalking = false;
                AI.isShooting = true;
                AI.hostileNPC = true;
            }
        }

        if (AI.nHealth.health <= 0)
        {
            AI.nHealth.StartCoroutine(AI.nHealth.NPCDeath());
        }
    }

    public void OnBecameInvisible()
    {
        AI.NPC.gameObject.AddComponent<RemoveNPC>();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
