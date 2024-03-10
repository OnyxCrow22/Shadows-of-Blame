using UnityEngine;

public class Idle : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    Vector3 direction;
    private PlayerMovementSM playsm;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.pAnim.SetBool("Walking", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.pAnim.SetBool("CrouchingIdle", true);
        }
    }
}
