using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PunchBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float timeUntilnextPunch;

    [SerializeField]
    private int numberOfpunchAnimations;

    private bool isPunching;
    private float currentPunchduration;
    private int punchAnimation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetPunch();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isPunching == false)
        {
            currentPunchduration += Time.deltaTime;

            if (currentPunchduration > timeUntilnextPunch && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isPunching = true;
                punchAnimation = Random.Range(1, numberOfpunchAnimations + 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetPunch();
        }

        animator.SetFloat("PunchAnimation", punchAnimation, 0.2f, Time.deltaTime);
    }

    private void ResetPunch()
    {
        if (isPunching)
        {
            punchAnimation--;
        }

        isPunching = false;
        currentPunchduration = 0;
        punchAnimation = 0;
    }
}
