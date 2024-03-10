using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    Vector3 direction;
    Vector3 rotation;
    private PlayerMovementSM playsm;

    public Walk(PlayerMovementSM playerStateMachine) : base("Walk", playerStateMachine)
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
        direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        playsm.speed = 6;

        playsm.rotation = new Vector3(0, horizontalInput * playsm.rotationSpeed * Time.deltaTime, 0);

        Vector3 move = new Vector3(0, 0, verticalInput);
        move = playsm.transform.TransformDirection(move);
        playsm.har.Move(move * playsm.speed * Time.deltaTime);
        playsm.transform.Rotate(playsm.rotation);

        playsm.cam.transform.position = playsm.player.transform.position;
        playsm.cam.transform.rotation = playsm.player.transform.rotation;

        if (direction.magnitude <= 0.01f)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Walking", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerStateMachine.ChangeState(playsm.runningState);
            playsm.anim.SetBool("Sprinting", true);
        }
    }
}
