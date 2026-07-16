using UnityEngine;

public class Idle : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private Vector3 physicsVelocity;
    private PlayerState pState;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
        pState = playsm.brainHub.playerState; // Access the PlayerState through the PlayerNexus
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

        if (playsm.moveInput.magnitude >= 0.2f)
        {
            if (pState.IsAiming)
            {
                // If the player is aiming, we might want to handle movement differently, but for now, we'll just keep them in idle.
                return;
            }

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
