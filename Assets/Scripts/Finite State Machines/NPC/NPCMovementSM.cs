using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public List<GameObject[]> neutralNPCList = new List<GameObject[]>();
    public List<GameObject[]> hostileNPCList = new List<GameObject[]>();

    public NPCHealth nHealth;
    public PoliceLevel police;
    public NPCGun hidden;

    [HideInInspector]
    public NPCIdle idleState;
    [HideInInspector]
    public NPCWalk walkingState;
    [HideInInspector]
    public NPCFlee fleeState;
    [HideInInspector]
    public NPCShoot fireState;
    [HideInInspector]
    public NPCAttack meleeState;
    private void Awake()
    {
        idleState = new NPCIdle(this);
        walkingState = new NPCWalk(this);
        fleeState = new NPCFlee(this);
        fireState = new NPCShoot(this);
       // meleeState = new NPCAttack(this);
    }

    public IEnumerator ScreamFlee()
    {
        NPC.SetDestination(transform.position);
        NPCAnim.SetBool("scream", true);
        yield return new WaitForSeconds(3);
        NPCAnim.SetBool("scream", false);
        ChangeState(fleeState);
        NPCAnim.SetBool("flee", true);
        isWalking = false;
        isFleeing = true;
    }

    public void SearchNPCS()
    {
        male = GameObject.FindGameObjectsWithTag("MaleNPC");
        female = GameObject.FindGameObjectsWithTag("FemaleNPC");

        if (male.Length > 0 && hostileNPC)
        {
            hostileNPCList.Add(male);
        }
        else if (male.Length > 0 && neturalNPC)
        {
            neutralNPCList.Add(male);
        }

        if (female.Length > 0 && hostileNPC)
        {
            hostileNPCList.Add(female);
        }
        else if (female.Length > 0 && neturalNPC)
        {
            neutralNPCList.Add(female);
        }

        if (male.Length > 0 || female.Length > 0 && neturalNPC)
        {
            NPCSound.Play();
        }

        if (nHealth.health <= 0 && male.Length > 0 && hostileNPC)
        {
            hostileNPCList.Remove(male);
        }
        else if (nHealth.health <= 0 && male.Length > 0 && neturalNPC)
        {
            neutralNPCList.Remove(male);
        }
        if (nHealth.health <= 0 && female.Length > 0 && hostileNPC)
        {
            hostileNPCList.Remove(female);
        }
        else if (nHealth.health <= 0 && female.Length > 0 && neturalNPC)
        {
            neutralNPCList.Remove(female);
        }
    }

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}
