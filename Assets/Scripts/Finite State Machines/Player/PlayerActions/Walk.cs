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

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref playsm.turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playsm.har.Move(moveDir * playsm.speed * Time.deltaTime);

        if (direction.magnitude < 0.01f)
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
