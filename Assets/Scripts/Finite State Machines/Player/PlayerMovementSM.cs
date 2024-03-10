using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController pChar;
    public float speed = 6f;
    public Animator pAnim;
    public Transform cam;
    public float turnSmooth = 0.1f;

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

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
