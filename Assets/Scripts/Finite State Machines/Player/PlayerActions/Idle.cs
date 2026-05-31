using UnityEngine;

public class Idle : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private Vector3 physicsVelocity;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        physicsVelocity = Vector3.zero;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        playsm.currentSpeed = Mathf.MoveTowards(playsm.currentSpeed, 0f, playsm.acceleration * Time.deltaTime);
        playsm.anim.SetFloat(playsm.forwardSpeedHash, playsm.currentSpeed, 0.1f, Time.deltaTime);

        if (playsm.moveInput.magnitude >= 0.2f && !playsm.weapon.aiming)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            return;
        }
        if (playsm.crouchPressed && !playsm.Crouched)
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool(playsm.crouchingHash, true);
            return;
        }
    }
}
