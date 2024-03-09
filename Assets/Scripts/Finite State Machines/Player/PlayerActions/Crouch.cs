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

        playerStateMachine.ChangeState(playsm.idleState);

        playerStateMachine.ChangeState(playsm.proningState);
    }
}
