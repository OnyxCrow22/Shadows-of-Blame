using UnityEngine;

public class CrouchWalking : PlayerBaseState
{
    float turnSmoothVelocity;
    Vector3 movementVelocity;
    private PlayerMovementSM playsm;

    public CrouchWalking(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        playsm.speed = 6f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Vector3 direction = new Vector3(playsm.moveInput.x, 0, playsm.moveInput.y).normalized;

        if (direction.magnitude <= 0.01f && playsm.Crouched)
        {
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool(playsm.crouchingWalkingHash, false); // Optimised for Garbage Collection
            return;
        }

        if (!playsm.crouchPressed && playsm.Crouched)
        {
            playsm.Crouched = false;

            playerStateMachine.ChangeState(playsm.walkingState);

            playsm.anim.SetBool(playsm.crouchingWalkingHash, false);
            playsm.anim.SetBool(playsm.crouchingHash, false);
            return;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        movementVelocity = moveDir.normalized * playsm.speed;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        float verticalVelocity = playsm.gravity * Time.deltaTime;

        Vector3 finalMovingFrame = new Vector3(movementVelocity.x, verticalVelocity, movementVelocity.z);

        playsm.har.Move(finalMovingFrame * Time.deltaTime);
    }
}
