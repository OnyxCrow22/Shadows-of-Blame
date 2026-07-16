using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    // The centralised brain
    public PlayerNexus brainHub;

    // bools
    public bool Crouched;
    public bool Jumping = false;
    public bool isShooting;
    public bool isGrounded = true;
    public bool isPunching;

    public bool throwingGrenade = false;
    public bool hasThrownGrenade = false;

    [Header("Input Checks")]
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 lookInput;
    [HideInInspector] public bool attackPressed;
    [HideInInspector] public bool weaponEquipPressed;
    [HideInInspector] public bool sprintPressed;
    [HideInInspector] public bool crouchPressed;
    [HideInInspector] public float turnSmoothVelocity;

    public static event Action OnShowRegionPressed;
    public static event Action OnSprintPressed;
    public static event Action OnSprintEnded;
    public static event Action OnWalkPressed;
    public static event Action OnWalkEnded;
    public static event Action OnJumped;
    public static event Action OnLanded;

    public static event Action OnPunch;
    public static event Action OnShot;

    public float controllerSensitvity = 100f;
    public float acceleration = 10f;
    public float currentSpeed;


    // States
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
    public Punch punchingState;

    // Hashed animations
    [Header("Animation Hashes")]
    [HideInInspector] public int forwardSpeedHash;
    [HideInInspector] public int crouchingHash;
    [HideInInspector] public int crouchingWalkingHash;
    [HideInInspector] public int idleHash;
    [HideInInspector] public int walkingHash;
    [HideInInspector] public int runningHash;
    [HideInInspector] public int jumpingHash;
    [HideInInspector] public int firingHash;
    [HideInInspector] public int punchingHash;
    [HideInInspector] public int aimingHash;

    private void Awake()
    {
        // Animation hashes
        forwardSpeedHash = Animator.StringToHash("ForwardSpeed");
        crouchingHash = Animator.StringToHash("Crouching");
        crouchingWalkingHash = Animator.StringToHash("CrouchWalk");
        idleHash = Animator.StringToHash("Idle");
        walkingHash = Animator.StringToHash("Walk");
        runningHash = Animator.StringToHash("Sprint");
        jumpingHash = Animator.StringToHash("Jump");
        firingHash = Animator.StringToHash("Shoot");
        punchingHash = Animator.StringToHash("Punch");
        aimingHash = Animator.StringToHash("Aim");

        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        crouchWalking = new CrouchWalking(this);
        jumpingState = new Jump(this);
        punchingState = new Punch(this);

        brainHub = GetComponent<PlayerNexus>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isGrounded && !Jumping)
        {
            ChangeState(jumpingState);
        }
    }

    public void OnShowRegion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (OnShowRegionPressed != null)
            {
                OnShowRegionPressed.Invoke();
            }
            else
            {
                Debug.LogWarning("No subscribers for OnShowRegionPressed event.");
            }
        }
    }

    public void TriggerSprintSound() => OnSprintPressed?.Invoke();

    public void TriggerSprintEnd() => OnSprintEnded?.Invoke();

    public void TriggerWalkSound() => OnWalkPressed?.Invoke();

    public void TriggerWalkEnd() => OnWalkEnded?.Invoke();

    public void TriggerJumpPerformed() => OnJumped?.Invoke();
    
    public void TriggerLanded() => OnLanded?.Invoke();

    public void TriggerPunchSound() => OnPunch?.Invoke();

    public void TriggerShotSound() => OnShot?.Invoke();

    public void OnAttack(InputAction.CallbackContext context)
    {
        /*
        if (!context.performed) return;

        if (weapon.gunEquipped && !isShooting)
        {
            isShooting = true;
            ChangeState(firingState);
            AudioManager.manager.Play("shootGun");
            anim.SetBool(firingHash, true);
        }
        else if (!weapon.gunEquipped && !isPunching)
        {
            isPunching = true;
            ChangeState(punchingState);
            AudioManager.manager.Play("Punch");
            anim.SetBool(punchingHash, true);
        }
        */
    }

    /*
    public void OnWeaponEquip(InputAction.CallbackContext context)
    {
        if (context.performed && weapon.pressCount == 0)
        {
            weapon.ammoText.gameObject.SetActive(true);
            weapon.gun.SetActive(true);
            weapon.reticle.SetActive(true);
            weapon.pressCount = 1;
            weapon.gunEquipped = true;
            AudioManager.manager.Play("equipGun");
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed && weapon.gunEquipped)
        {
            PlayerState.IsAiming = true;
        }

        if (context.canceled)
        {
            PlayerState.IsAiming = false;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed && !throwingGrenade && !hasThrownGrenade)
        {
            throwingGrenade = true;
        }
    }

    */

    public void OnSprint(InputAction.CallbackContext context)
    {
        // Sprint input handled in Walk state
        if (context.performed) sprintPressed = true;
        if (context.canceled) sprintPressed = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        // Crouching handled in CrouchWalk state
        if (context.performed) crouchPressed = true;
        if (context.canceled) crouchPressed = false;
    }

    // TO DO
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }
    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
