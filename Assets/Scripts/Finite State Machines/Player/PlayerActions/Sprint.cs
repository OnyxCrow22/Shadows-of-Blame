using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    float turnVelocity;
    Vector3 direction;
    private PlayerMovementSM playsm;

    public Sprint(PlayerMovementSM playerStateMachine) : base("Sprint", playerStateMachine)
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

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.pChar.transform.eulerAngles.y, targetAngle, ref turnVelocity, playsm.turnSmooth);
        playsm.pChar.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playsm.pChar.Move(moveDir * playsm.speed * Time.deltaTime);

        if (!Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.pAnim.SetBool("Sprinting", false);
            playsm.speed = 6;
        }
    }
}
