using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchWalking : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public CrouchWalking(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
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

        playerStateMachine.ChangeState(playsm.crouchingState);
    }
}
