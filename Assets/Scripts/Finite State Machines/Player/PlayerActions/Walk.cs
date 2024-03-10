using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    float turnVelocity;
    Vector3 direction;
    private PlayerMovementSM playsm;

    public Walk(PlayerMovementSM playerStateMachine) : base("Walk", playerStateMachine)
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

        playsm.speed = 6;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.pChar.transform.eulerAngles.y, targetAngle, ref turnVelocity, playsm.turnSmooth);
        playsm.pChar.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playsm.pChar.Move(moveDir * playsm.speed * Time.deltaTime);

        if (direction.magnitude < 0.01f)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.pAnim.SetBool("Walking", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerStateMachine.ChangeState(playsm.runningState);
            playsm.pAnim.SetBool("Sprinting", true);
            playsm.speed = 12;
        }
    }
}
