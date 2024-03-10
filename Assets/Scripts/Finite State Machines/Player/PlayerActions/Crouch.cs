using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    private PlayerMovementSM playsm;

    public Crouch(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0;
        verticalInput = 0;
        playsm.speed = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            playsm.Crouched = false;
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Crouching", false);
            playsm.speed = 0;
        }

        playerStateMachine.ChangeState(playsm.crouchWalking);
    }
}
