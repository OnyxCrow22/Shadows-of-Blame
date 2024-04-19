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

    private Gun gun;

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

        if (direction.magnitude >= 0.1f && playsm.weapon.aiming == false)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.anim.SetBool("Walking", true);
            AudioManager.manager.Play("walk");
            playsm.speed = 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && playsm.Crouched == false)
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("Crouching", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.jumpingState);
            playsm.anim.SetBool("Jump", true);
            playsm.isGrounded = false;
            playsm.Jumping = true;
        }

        if (Input.GetMouseButton(0) && playsm.weapon.gunEquipped == true)
        {
            playerStateMachine.ChangeState(playsm.firingState);
            playsm.anim.SetBool("shoot", true);
            playsm.isShooting = true;
        }

        if (Input.GetMouseButton(0) && playsm.weapon.gunEquipped == false)
        {
            playerStateMachine.ChangeState(playsm.punchingState);
            playsm.isPunching = true;
            playsm.anim.SetBool("punching", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && playsm.weapon.pressCount == 0)
        {
            playsm.weapon.ammoText.gameObject.SetActive(true);
            playsm.weapon.gun.SetActive(true);
            playsm.weapon.reticle.SetActive(true);
            playsm.weapon.pressCount = 1;
            playsm.weapon.gunEquipped = true;
            AudioManager.manager.Play("equipGun");
        }

    }
}
