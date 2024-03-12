using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : PlayerBaseState
{
    Vector3 velocity;
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

        playsm.isGrounded = Physics.CheckSphere(playsm.groundCheck.position, playsm.groundDistance, playsm.ground); 

        if(playsm.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (Input.GetKey(KeyCode.Space) && playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.jumpingState);
            playsm.anim.SetBool("Jump", true);
            playsm.isGrounded = false;
        }
    }
}
