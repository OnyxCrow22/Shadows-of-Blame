using UnityEngine;

public class Idle : PlayerBaseState
{
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.pAnim.SetBool("Walking", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.pAnim.SetBool("Crouching", true);
        }
    }
}
