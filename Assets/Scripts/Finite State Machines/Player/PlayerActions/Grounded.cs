using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Grounded(PlayerMovementSM playerStateMachine) : base("Grounded", playerStateMachine)
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

        playerStateMachine.ChangeState(playsm.jumpingState);
    }
}
