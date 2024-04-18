using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Punch(PlayerMovementSM playerStateMachine) : base("Punch", playerStateMachine)
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

        playsm.ppunchAnim.RandomAnimation();

        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerStateMachine.ChangeState(playsm.idleState);

            playsm.isPunching = false;
        }
    }
}
