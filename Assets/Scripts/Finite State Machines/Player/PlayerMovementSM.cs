using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController har;
    public float speed;
    public float turnSmoothTime = 0.1f;
    public Animator anim;
    public Transform cam;
    public Transform player;

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
    public CrouchWalking crouchWalking;
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
        crouchWalking = new CrouchWalking(this);
        jumpingState = new Jump(this);
        groundedState = new Grounded(this);
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
