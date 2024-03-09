using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Shoot(PlayerMovementSM playerStateMachine) : base("Shoot", playerStateMachine)
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
