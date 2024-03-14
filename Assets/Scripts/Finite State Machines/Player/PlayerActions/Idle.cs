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
            playsm.speed = 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && playsm.Crouched == false)
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("Crouching", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerStateMachine.ChangeState(playsm.jumpingState);
            playsm.anim.SetBool("Jump", true);
            playsm.Jumping = true;
        }

        if (Input.GetMouseButton(0))
        {
            playsm.weapon.gun.SetActive(true);
            playerStateMachine.ChangeState(playsm.firingState);
            playsm.anim.SetBool("shoot", true);
            playsm.isShooting = true;
        }

    }
}
