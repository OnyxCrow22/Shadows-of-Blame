using UnityEngine;

public class PunchBehaviour : StateMachineBehaviour
{
    [SerializeField] private float timeUntilNextPunch = 0.5f;
    [SerializeField] private int numberOfPunchAnimations = 3;

    private bool isPunching;
    private float currentPunchDuration;
    private int punchAnimationIndex;

    // Cached hash value for performance optimization
    private static readonly int PunchAnimParamHash = Animator.StringToHash("PunchAnimation");

    // Called when the state machine starts evaluating this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetPunch();
    }

    // Called on each Update frame between OnStateEnter and OnStateExit
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If we are currently transitioning out of this state, stop processing loop logic
        if (animator.IsInTransition(layerIndex)) return;

        if (!isPunching)
        {
            currentPunchDuration += Time.deltaTime;

            // Checked if the required window has passed. 
            // Instead of checking exact frame percentages, we check if the animation has cycled.
            if (currentPunchDuration > timeUntilNextPunch)
            {
                isPunching = true; // Allow punching
                punchAnimationIndex = Random.Range(1, numberOfPunchAnimations + 1);

                // Set the parameter once when the state changes, rather than every frame
                animator.SetFloat(PunchAnimParamHash, punchAnimationIndex, 0.2f, Time.deltaTime);
            }
        }
        else
        {
            // If the loop has naturally completed a cycle, automatically reset the punch window
            if (stateInfo.normalizedTime >= 1.0f)
            {
                ResetPunch();
                animator.SetFloat(PunchAnimParamHash, 0f);
            }
        }
    }

    private void ResetPunch()
    {
        isPunching = false;
        currentPunchDuration = 0f;
        punchAnimationIndex = 0;
    }
}