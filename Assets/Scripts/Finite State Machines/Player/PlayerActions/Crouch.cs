using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Crouch(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Crouching", false);
        }

        playerStateMachine.ChangeState(playsm.crouchWalking);
    }
}
