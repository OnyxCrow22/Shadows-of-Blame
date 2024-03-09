using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Sprint(PlayerMovementSM playerStateMachine) : base("Sprint", playerStateMachine)
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

        playerStateMachine.ChangeState(playsm.walkingState);
    }
}
