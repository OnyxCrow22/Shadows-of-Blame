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

        if (playsm.har.isGrounded)
        {
            velocity.y = 2f;
            velocity.y = Mathf.Sqrt(playsm.jumpHeight * -2f * playsm.gravity);
        }

        velocity.y += playsm.gravity * Time.deltaTime;

        playsm.har.Move(velocity * Time.deltaTime);

        if (playsm.har.isGrounded)
        {
            velocity.y = 0;
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Jump", false);
            playsm.Jumping = false;
            playsm.isGrounded = true;
        }
    }
}