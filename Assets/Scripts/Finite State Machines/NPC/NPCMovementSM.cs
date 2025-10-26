using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementSM : NPCStateMachine
{
    public NavMeshAgent NPC;
    public GameObject player;
    public GameObject hiddenGun;
    public PlayerMovementSM playsm;
    public RemoveNPC removed;
    public Animator NPCAnim;
    public GameObject NPCFOV;
    public AudioSource NPCSound;
    public AudioClip[] clips;
    GameObject[] male;
    GameObject[] female;

    public bool spawnedIn = false;
    public bool isWalking = false;
    public bool isFleeing = false;
    public bool isAttacking = false;
    public bool isShooting = false;
    public bool canReturn = false;
    public bool hostileNPC = false;
    public bool neturalNPC = false;
    public bool arrived = false;

    public bool isMale = false;
    public bool isFemale = false;

    public int aggression;

    public NPCHealth nHealth;
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
    // DEPRACATED SCRIPTS - DO NOT ADD
    [HideInInspector]
    public NPCAttack meleeState;
    private void Awake()
    {
        idleState = new NPCIdle(this);
        walkingState = new NPCWalk(this);
        fleeState = new NPCFlee(this);
        fireState = new NPCShoot(this);

        // DEPRACATED SCRIPTS - DO NOT ADD

        // meleeState = new NPCAttack(this);

        aggression = Random.Range(0, 2);
    }

    public IEnumerator ScreamFlee()
    {
        if (neturalNPC)
        {
            NPC.isStopped = true;
            NPCAnim.SetBool("scream", true);
            yield return new WaitForSeconds(3);
            NPCAnim.SetBool("scream", false);
            NPC.SetDestination(walking.newPosition);
            ChangeState(fleeState);
            NPC.isStopped = false;
            NPCAnim.SetBool("flee", true);
            walking.FleeFromPlayer();
            isWalking = false;
            isFleeing = true;

            if (isFleeing)
            {
                StartCoroutine(ReturnDelay());
                canReturn = false;
            }
        }
    }

    public IEnumerator ReturnDelay()
    {
        if (!canReturn)
        {
            canReturn = false;
            yield return new WaitForSeconds(20);
            canReturn = true;
            NPC.SetDestination(walking.currentPedestrianNode.transform.position);
            yield break;
        }
    }

    public void SearchNPCS()
    {
        male = GameObject.FindGameObjectsWithTag("MaleNPC");
        female = GameObject.FindGameObjectsWithTag("FemaleNPC");

        if (male.Length > 0 || female.Length > 0 && neturalNPC)
        {
            NPCSound.Play();
        }
    }

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}
