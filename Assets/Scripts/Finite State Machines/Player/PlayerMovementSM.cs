using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController har;
    public float speed;
    public float turnSmoothTime;
    public float gravity;
    public float jumpHeight;
    public float groundDistance;
    public LayerMask ground;
    public Animator anim;
    public Transform cam;
    public Transform groundCheck;
    public Transform player;
    public bool Crouched;
    public bool Jumping = false;
    public bool isShooting;

    // Scripts
    public Gun weapon;

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

    private void Awake()
    {
        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        crouchWalking = new CrouchWalking(this);
        jumpingState = new Jump(this);
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
