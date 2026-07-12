using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementSM : NPCStateMachine
{
    [Header("References")]
    public NavMeshAgent NPC;
    public GameObject player;
    public GameObject hiddenGun;
    public PlayerMovementSM playsm;
    public RemoveNPC removed;
    public Animator NPCAnim;
    public GameObject NPCFOV;
    public AudioSource NPCSound;
    public AudioClip[] clips;

    [Header("State Flags")]
    public bool spawnedIn = false;
    public bool isWalking = false;
    public bool isFleeing = false;
    public bool isAttacking = false;
    public bool isShooting = false;
    public bool canReturn = false;
    public bool hostileNPC = false;
    public bool neturalNPC = false;

    [Header("Stats")]
    public int aggression; // Flee or fight against the player
    public HealthSystem nHealth;
    public PoliceLevel police;
    public NPCGun hidden;
    public FollowWaypoints walking;

    [HideInInspector]
    public NPCIdle idleState;
    [HideInInspector]
    public NPCWalk walkingState;
    [HideInInspector]
    public NPCFlee fleeState;
    [HideInInspector]
    public NPCShoot fireState;

    private void Awake()
    {
        idleState = new NPCIdle(this);
        walkingState = new NPCWalk(this);
        fleeState = new NPCFlee(this);
        fireState = new NPCShoot(this);

        // Randomize personality upon spawn
        aggression = (Random.value < 0.20f) ? 1 : 0;
    }

    public IEnumerator ScreamFlee()
    {
        if (neturalNPC && !isFleeing)
        {
            isFleeing = true; // Set flag immediately to prevent re-entry

            NPC.isStopped = true;
            NPCAnim.SetBool("scream", true);
            yield return new WaitForSeconds(3f);

            NPCAnim.SetBool("scream", false);
            NPCAnim.SetBool("flee", true);

            NPC.SetDestination(walking.newPosition);
            NPC.isStopped = false;

            walking.FleeFromPlayer();

            // Trigger state transition only after scream finishes
            ChangeState(fleeState);

            StartCoroutine(ReturnDelay());
        }
    }

    public IEnumerator ReturnDelay()
    {
        canReturn = false;
        yield return new WaitForSeconds(20f);
        canReturn = true;

        // Reset state back to walking once calm
        NPC.SetDestination(walking.currentPedestrianNode.transform.position);
        ChangeState(walkingState);
    }

    public void SearchNPCS()
    {
        if (neturalNPC)
        {
            NPCSound.Play();
        }
    }

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}