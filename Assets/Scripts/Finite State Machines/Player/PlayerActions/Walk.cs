using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Walk(PlayerMovementSM playerStateMachine) : base("Walk", playerStateMachine)
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

        playerStateMachine.ChangeState(playsm.runningState);
    }
}
