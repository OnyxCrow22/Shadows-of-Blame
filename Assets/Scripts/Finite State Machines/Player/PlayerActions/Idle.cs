using UnityEngine;

public class Idle : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    private PlayerMovementSM playsm;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
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
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.anim.SetBool("Walking", true);
            playsm.speed = 6;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("Crouching", true);
        }
    }
}
