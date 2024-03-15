
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

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("shoot", false);
            playsm.isShooting = false;
        } 
    }
}
