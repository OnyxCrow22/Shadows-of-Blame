using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementSM : NPCStateMachine
{
    public GameObject NPC;
    public PlayerMovementSM playsm;
    public RemoveNPC removed;
    public SpawnNPC spawning;

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
       // fleeState = new NPCFlee(this);
       // fireState = new NPCShoot(this);
       // meleeState = new NPCAttack(this);
    }

    protected override NPCBaseState GetInitialState()
    {
        return idleState;
    }
}
