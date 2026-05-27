using UnityEngine;

public class Jump : PlayerBaseState
{
    Vector3 airMovementVelocity;
    private PlayerMovementSM playsm;

    public Jump(PlayerMovementSM playerStateMachine) : base("Jump", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        float jumpForce = Mathf.Sqrt(playsm.jumpHeight * -2f * playsm.gravity);

        playsm.currentSpeed = jumpForce;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Vector3 jumpDirection = new Vector3(playsm.moveInput.x, 0f, playsm.moveInput.y).normalized;

        float airSteerSpeed = 4f;
        Vector3 targetAngleDirection = Quaternion.Euler(0f, playsm.cam.eulerAngles.y, 0f) * jumpDirection;
        airMovementVelocity = targetAngleDirection.normalized * airSteerSpeed;

        if (playsm.currentSpeed < 0f && playsm.har.isGrounded)
        {
            playsm.currentSpeed = 0;
            playsm.Jumping = false;
            playsm.isGrounded = true;

            if (playsm.moveInput.magnitude >= 0.2f)
            {
                playerStateMachine.ChangeState(playsm.walkingState);
            }
            else
            {
                playerStateMachine.ChangeState(playsm.idleState);
            }

            playsm.anim.SetBool(playsm.jumpingHash, false);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        playsm.currentSpeed += playsm.gravity * Time.deltaTime;

        Vector3 finalAirFrameVectors = new Vector3(airMovementVelocity.x, playsm.currentSpeed, airMovementVelocity.z);

        playsm.har.Move(finalAirFrameVectors * Time.deltaTime);
    }
}