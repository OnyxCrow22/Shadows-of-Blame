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
        velocity.y = Mathf.Sqrt(playsm.jumpHeight * 1f * playsm.gravity);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        velocity.y -= playsm.gravity * Time.deltaTime;

        playsm.har.Move(velocity * Time.deltaTime);

        if (playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Jump", false);
            playsm.Jumping = false;
        }
    }
}
