using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchWalking : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    float turnSmoothVelocity;
    Vector3 direction;
    Vector3 velocity;
    private PlayerMovementSM playsm;

    public CrouchWalking(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
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

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playsm.har.Move(moveDir.normalized * playsm.speed * Time.deltaTime);

        velocity.y += playsm.gravity * Time.deltaTime;

        playsm.har.Move(velocity * Time.deltaTime);

        if (direction.magnitude <= 0.01f && playsm.Crouched == true)
        {
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("CrouchWalk", false);
        }
    }
}
