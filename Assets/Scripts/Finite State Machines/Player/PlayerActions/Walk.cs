using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerBaseState
{
    float turnSmoothVelocity;
    private PlayerMovementSM playsm;
    private Vector3 movementVelocity;

    public Walk(PlayerMovementSM playerStateMachine) : base("Walk", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.manager.Play("walk");
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.manager.Stop("walk");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Vector3 direction = new Vector3(playsm.moveInput.x, 0f, playsm.moveInput.y).normalized;

        if (direction.magnitude < 0.1f)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            return;
        }
        if (playsm.sprintPressed && playsm.currentStaminaLevel >= 10) // Can we sprint?
        {
            playerStateMachine.ChangeState(playsm.runningState);
            return;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref playsm.turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        playsm.currentSpeed = Mathf.MoveTowards(playsm.currentSpeed, 3f, playsm.acceleration * Time.deltaTime);
        playsm.anim.SetFloat(playsm.forwardSpeedHash, playsm.currentSpeed, 0.1f, Time.deltaTime);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        movementVelocity = moveDir.normalized * playsm.currentSpeed;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Vector3 finalWalkFrame = new Vector3(movementVelocity.x, playsm.gravity, movementVelocity.z);

        playsm.har.Move(finalWalkFrame * Time.deltaTime);
    }
}
