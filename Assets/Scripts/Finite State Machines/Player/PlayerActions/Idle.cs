using UnityEngine;

public class Idle : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    private Gun gun;

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        playsm.currentSpeed = Mathf.MoveTowards(playsm.currentSpeed, 0f, playsm.acceleration * Time.deltaTime);
        playsm.anim.SetFloat("ForwardSpeed", playsm.currentSpeed, 0.1f, Time.deltaTime);

        Vector3 velocity = Vector3.zero;
        velocity.y += playsm.gravity;
        playsm.har.Move(velocity * Time.deltaTime);

        playsm.anim.SetFloat("ForwardSpeed", 0, 0.1f, Time.deltaTime);

        if (playsm.moveInput.magnitude >= 0.2f && !playsm.weapon.aiming)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && playsm.Crouched == false)
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("Crouching", true);
        }

        if (playsm.jumpPressed && playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.jumpingState);
            playsm.anim.SetBool("Jump", true);
            playsm.isGrounded = false;
            playsm.Jumping = true;
        }

        if (playsm.attackPressed && playsm.weapon.gunEquipped == true)
        {
            playerStateMachine.ChangeState(playsm.firingState);
            AudioManager.manager.Play("shootGun");
            playsm.anim.SetBool("shoot", true);
            playsm.isShooting = true;
        }

        if (playsm.attackPressed && playsm.weapon.gunEquipped == false)
        {
            playerStateMachine.ChangeState(playsm.punchingState);
            playsm.isPunching = true;
            AudioManager.manager.Play("Punch");
            playsm.anim.SetBool("punching", true);
        }

        if (playsm.weaponEquipPressed && playsm.weapon.pressCount == 0)
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
