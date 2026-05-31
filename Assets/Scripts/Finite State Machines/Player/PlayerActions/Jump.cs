using UnityEngine;

public class Jump : PlayerBaseState
{
    Vector3 airMovementVelocity;
    private PlayerMovementSM playsm;

    private float jumpDelay;
    private float verticalVelocity;
    private Vector3 velocityTracked;

    public Jump(PlayerMovementSM playerStateMachine) : base("Jump", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        playsm.isGrounded = false;
        playsm.Jumping = true;

        playsm.anim.SetBool(playsm.jumpingHash, true);

        Vector3 groundDirection = new Vector3(playsm.moveInput.x, 0f, playsm.moveInput.y).normalized;
        Vector3 targetAngleDirection = Quaternion.Euler(0f, playsm.cam.eulerAngles.y, 0f) * groundDirection;

        // Stop the player from flying forward
        float momentumReduce = 0.55f;
        velocityTracked = targetAngleDirection * (playsm.currentSpeed * momentumReduce);

        // Add JumpForce
        float jumpForce = Mathf.Sqrt(playsm.jumpHeight * -2f * playsm.gravity);
        verticalVelocity = jumpForce;

        // Add a delay of 0.15 seconds before jumping again
        jumpDelay = 0.15f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (jumpDelay > 0)
        {
            jumpDelay -= Time.deltaTime;
        }

        Vector3 jumpDirection = new Vector3(playsm.moveInput.x, 0f, playsm.moveInput.y).normalized;
        Vector3 targetAngleDirection = Quaternion.Euler(0f, playsm.cam.eulerAngles.y, 0f) * jumpDirection;

        if (velocityTracked.magnitude > 0.1f)
        {
            float airSteerSpeed = 1.5f;
            Vector3 activeAir = targetAngleDirection * airSteerSpeed;
            airMovementVelocity = velocityTracked + activeAir;
        }
        else
        {
            float airSteerSpeed = 4f;
            airMovementVelocity = targetAngleDirection * airSteerSpeed;
        }

        if (verticalVelocity <= -1f && playsm.har.isGrounded && jumpDelay <= 0)
        {
            playsm.Jumping = false;
            playsm.isGrounded = true;

            if (playsm.moveInput.magnitude >= 0.2f)
            {
                playsm.currentSpeed = playsm.sprintPressed ? 8f : 3f;
                playerStateMachine.ChangeState(playsm.walkingState);
            }
            else
            {
                playsm.currentSpeed = 0;
                playerStateMachine.ChangeState(playsm.idleState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();


        // Apply gravity to the player
        verticalVelocity += playsm.gravity * Time.deltaTime;

        Vector3 finalAirFrameVectors = new Vector3(airMovementVelocity.x, verticalVelocity, airMovementVelocity.z);

        playsm.har.Move(finalAirFrameVectors * Time.deltaTime);
    }
    public override void Exit()
    {
        base.Exit();

        playsm.anim.SetBool(playsm.jumpingHash, false);
    }
}