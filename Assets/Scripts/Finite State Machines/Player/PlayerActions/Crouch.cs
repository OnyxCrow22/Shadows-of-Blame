using UnityEngine;

public class Crouch : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private Vector3 direction;

    public Crouch(PlayerMovementSM playerStateMachine) : base("Crouch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        playsm.speed = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        direction = new Vector3(playsm.moveInput.x, 0f, playsm.moveInput.y).normalized;

        if (!playsm.crouchPressed && playsm.Crouched == true)
        {
            playsm.Crouched = false;
            playerStateMachine.ChangeState(playsm.idleState);

            playsm.anim.SetBool(playsm.crouchingHash, false);
        }

        if (direction.magnitude > 0.01f && playsm.Crouched == true)
        {
            playerStateMachine.ChangeState(playsm.crouchWalking);
            playsm.anim.SetBool(playsm.crouchingWalkingHash, true);
            playsm.speed = 6;
        }
    }
}
