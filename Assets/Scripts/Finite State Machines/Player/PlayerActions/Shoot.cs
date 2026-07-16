
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private bool durationCheck;
    private float stateTimer;

    private PlayerState pState;

    public Shoot(PlayerMovementSM playerStateMachine) : base("Shoot", playerStateMachine)
    {
        playsm = playerStateMachine;
        pState = playsm.brainHub.playerState; // Access the PlayerState through the PlayerNexus
    }

    public override void Enter()
    {
        base.Enter();

        durationCheck = false;
        stateTimer = 0;
        playsm.currentSpeed = 0;

        playsm.TriggerShotSound();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!durationCheck)
        {

            if (playsm.anim.IsInTransition(0))
            {
                return;
            }

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

                if (playsm.moveInput.magnitude > 0.2f)
                {
                    playerStateMachine.ChangeState(playsm.walkingState);
                }
                else
                {
                    playerStateMachine.ChangeState(playsm.idleState);
                }
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
