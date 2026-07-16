using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private bool durationCheck;
    private float stateTimer;
    public int targetLayer = 0;

    public Punch(PlayerMovementSM playerStateMachine) : base("Punch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        playsm.currentSpeed = 0;
        durationCheck = false;
        stateTimer = 0;
        playsm.TriggerPunchSound();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Query with the Animator
        if (!durationCheck)
        {
            if (playsm.anim.IsInTransition(0))
            {
                return;
            }

            AnimatorStateInfo stateInformation = playsm.anim.GetCurrentAnimatorStateInfo(0);

            if (stateInformation.shortNameHash == playsm.punchingHash)
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
                playsm.isPunching = false;
                AudioManager.manager.Stop("Punch");

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

        Vector3 gravityMove = new Vector3(0f, playsm.gravity, 0f);
        playsm.har.Move(gravityMove * Time.deltaTime);
    }
}
