using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Jump(PlayerMovementSM playerStateMachine) : base("Jump", playerStateMachine)
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

        playerStateMachine.ChangeState(playsm.idleState);
    }
}
