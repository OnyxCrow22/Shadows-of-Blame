using System.Collections;
using System.Collections.Generic;
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

    public bool spawnedIn = false;
    public bool isWalking = false;
    public bool isFleeing = false;
    public bool isAttacking = false;

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

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}
