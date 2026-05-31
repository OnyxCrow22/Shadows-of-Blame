using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // bools
    public bool Crouched;
    public bool Jumping = false;
    public bool isShooting;
    public bool isPlayerDead = false;
    public bool inVehicle = false;
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
    public float controllerSensitvity = 100f;
    public float acceleration = 10f;
    public float currentSpeed;

    public Gun weapon;
    public PlayerHealth health;
    public PunchSystem punching;

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

    // Stamina System
    [Header("Stamina System")]
    [SerializeField] private float maxStamina = 100; // Max amount of stamina initially
    [SerializeField] private float staminaRegenerationRate = 15; // How fast stamina regenerates after using it
    [SerializeField] private float staminaRegerationDelay = 1; // Delay regeneration by one second

    public float currentStaminaLevel { get; private set; } // Get and set the current stamina level of the player
    private float regenerationCooldownTimer; // How long until the player can regenerate again?

    protected override void Start()
    {
        base.Start();
        currentStaminaLevel = maxStamina; // The current stamina is set to 100 at the start of the level load.

    }

    protected override void Update()
    {
        base.Update();

        DelayStaminaRegeneration();
    }

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

        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        crouchWalking = new CrouchWalking(this);
        jumpingState = new Jump(this);
        punchingState = new Punch(this);
    }

    /// <summary>
    /// Actions that defines how the player moves around the level, interacts with it, and how they attack enemies in the level.
    /// </summary>
    /// <param name="context"></param>

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        float jumpCost = 15; // Jumping is not free

        if (isGrounded && !Jumping)
        {
            if (ConsumeStamina(jumpCost)) // Does the player have enough stamina to pay the jump tax?
            {
                ChangeState(jumpingState); // JUMP!
            }
        }
        else
        {
            // Player cannot pay jump tax.
            Debug.Log("Cannot jump! Out of energy");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
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
    }

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
            weapon.aiming = true;
        }

        if (context.canceled)
        {
            weapon.aiming = false;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed && !throwingGrenade && !hasThrownGrenade)
        {
            throwingGrenade = true;
        }
    }

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

        if (!inVehicle)
        {

        }
        else
        {

        }
    }

    /// <summary>
    /// The stamina system, which decreases as the player runs
    /// </summary>
    /// <returns></returns>
    /// 
    public bool ConsumeStamina(float staminaAmount)
    {
        if (currentStaminaLevel >= staminaAmount) // Is the current staminaLevel more than the maximum stamina amount?
        {
            currentStaminaLevel -= staminaAmount; // We need to decrease the stamina, as the player is getting tired.
            regenerationCooldownTimer = staminaRegerationDelay; // Timer resets the delay clock
            return true;
        }
        return false; // Not applicable in this instance.
    }

    // Decrease stamina over-time, when walking, or running
    public void DepleteStamina(float amountPerMinute)
    {
        currentStaminaLevel = Mathf.Max(0f, currentStaminaLevel - (amountPerMinute * Time.deltaTime));
        regenerationCooldownTimer = staminaRegerationDelay; // Timer resets the delay clock
    }

    public void DelayStaminaRegeneration()
    {
        if (regenerationCooldownTimer > 0f) // Has the timer already been used?
        {
            regenerationCooldownTimer -= Time.deltaTime; // Start counting down
            return;
        }

        if (currentStaminaLevel < maxStamina) // Is the stamina level below 100?
        {
            currentStaminaLevel = Mathf.Min(maxStamina, currentStaminaLevel + (staminaRegenerationRate * Time.deltaTime));
        }
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
