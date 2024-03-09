using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController pChar;
    public float speed = 6f;
    public Animator pAnim;

    // Animation states
    const string PLAYER_IDLE = "Player Idle";
    const string PLAYER_WALK = "Player Walk";
    const string PLAYER_SPRINT = "Player Sprint";
    const string PLAYER_JUMP = "Player Jump";
    const string PLAYER_DEAD = "Player Dead";
    const string PLAYER_CROUCHEDIDLE = "Player CrouchedIdle";
    const string PLAYER_CROUCHEDWALK = "Player CrouchedWalk";

    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walk walkingState;
    [HideInInspector]
    public Sprint runningState;
    [HideInInspector]
    public Crouch crouchingState;
    [HideInInspector]
    public Shoot firingState;
    [HideInInspector]
    public Prone proningState;
    [HideInInspector]
    public Jump jumpingState;
    [HideInInspector]
    public Grounded groundedState;

    private void Awake()
    {
        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        proningState = new Prone(this);
        jumpingState = new Jump(this);
        groundedState = new Grounded(this);
    }

    void ChangeAnimationState(string newState)
    {
        pAnim.Play(newState);
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
