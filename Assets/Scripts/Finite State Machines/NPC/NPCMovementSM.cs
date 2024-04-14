using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementSM : NPCStateMachine
{
    public NavMeshAgent NPC;
    public GameObject player;
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

    public NPCHealth nHealth;

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
       // fireState = new NPCShoot(this);
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

        if (male.Length > 0 || female.Length > 0)
        {
            NPCSound.Play();
        }
    }

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}
