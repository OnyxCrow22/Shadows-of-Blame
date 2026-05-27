
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private bool durationCheck;
    private float stateTimer;

    public Shoot(PlayerMovementSM playerStateMachine) : base("Shoot", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        durationCheck = false;
        stateTimer = 0;
        playsm.currentSpeed = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!durationCheck)
        {
            AnimatorStateInfo stateInformation = playsm.anim.GetCurrentAnimatorStateInfo(0);

            if (stateInformation.shortNameHash == playsm.firingHash)
            {
                stateTimer = stateInformation.length;
                durationCheck = true;
            }
        }

        else
        {
            // Begin ticking down the timer.
            stateTimer -= Time.deltaTime;

            // Timer ran out
            if (stateTimer <= 0)
            {
                playsm.isShooting = false;
                playsm.anim.SetBool(playsm.firingHash, false);

                if (playsm.weapon.aiming)
                {
                    if (playsm.moveInput.magnitude >= 0.2f)
                    {
                        playerStateMachine.ChangeState(playsm.walkingState);
                    }
                    else
                    {
                        playerStateMachine.ChangeState(playsm.idleState);
                    }
                    return;
                }

                if (playsm.moveInput.magnitude > 0.2f)
                {
                    playerStateMachine.ChangeState(playsm.walkingState);
                }
                else
                {
                    playerStateMachine.ChangeState(playsm.idleState);
                }
                playsm.anim.SetBool(playsm.punchingHash, false);
                return;
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Vector3 gravityMove = new Vector3(0f, playsm.gravity * Time.deltaTime, 0f);
        playsm.har.Move(gravityMove * Time.deltaTime);
    }
}
