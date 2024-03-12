using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerBaseState
{
    Vector3 velocity;
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

        playsm.isGrounded = Physics.CheckSphere(playsm.groundCheck.position, playsm.groundDistance, playsm.ground);

        if(playsm.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y = Mathf.Sqrt(playsm.jumpHeight * -2f * playsm.gravity);

        if (playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Jump", false);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
