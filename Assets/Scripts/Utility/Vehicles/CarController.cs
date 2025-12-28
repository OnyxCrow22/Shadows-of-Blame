using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    float horizontalInput;
    float verticalInput;
    float steeringAngle;
    public float maxSteeringAngle;
    public float motorForce;
    public float brakeForce;
    public float maxSpeed;
    float currentSpeed;
    public float indicator;
    public int pressCount;

    // Caches the last recorded speed.
    private float lastDisplayedSpeed = -1f;

    public Rigidbody target;
    public TextMeshProUGUI speedText;
    public Light leftIndicator, rightIndicator;
    public Light rearLight1, rearLight2;
    public Light[] reverseLight;

    [Header("Wheel colliders")]
    public Wheel[] wheels;

    [Header("Booleans")]
    public bool braking = false;
    public bool turningLeft = false;
    public bool turningRight = false;
    public bool indicating = false;
    public bool reversing = false;
    public bool vehicleonSide = false;

    [Header("Float references")]
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI Elements")]
    public RectTransform needle;
    public GameObject speedometer;

    [Header("Player Controller")]
    public PlayerInput pController;

    private Coroutine leftIndicatorRoutine;
    private Coroutine rightIndicatorRoutine;

    [System.Serializable] 
   public struct Wheel
    {
        public WheelCollider wCollider;
        public Transform vehicle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentSpeed = 0;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateSpeed();

        if (currentSpeed > 0.01f)
        {
            UpdateWheelPoses();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        vehicleonSide = Mathf.Abs(transform.up.y) < 0.2f;
    }

    public void OnFlip(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (vehicleonSide)
            FlipCar();
    }

    public void OnBrake(InputAction.CallbackContext ctx)
    {
        float brakeVal = ctx.ReadValue<float>();

        if (brakeVal > 0.01f)
        {
            braking = true;
            ApplyBrake(brakeVal);
        }
        else
        {
            braking = false;
            ReleaseBrake();
        }
    }

    public void OnReverse(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            BeginReversing();
        }
        else if (ctx.canceled)
        {
            StopReversing();
        }
    }

    public void OnAccelerate(InputAction.CallbackContext ctx)
    {
        verticalInput = ctx.ReadValue<float>();
    }

    public void OnIndicateLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (turningRight)
            // Turn off the right indicator
            ToggleIndicator(ref turningRight, TurningRight(), ref rightIndicatorRoutine, rightIndicator);

        // Turn on the left indicator
        ToggleIndicator(ref turningLeft, TurningLeft(), ref leftIndicatorRoutine, leftIndicator);
    }

    public void OnIndicateRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (turningLeft)
            // Turn off the left indicator
            ToggleIndicator(ref turningLeft, TurningLeft(), ref leftIndicatorRoutine, leftIndicator);

        // Turn on the right indicator
        ToggleIndicator(ref turningRight, TurningRight(), ref rightIndicatorRoutine, rightIndicator);
    }

    public void OnSteer(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();

        // Adds a deadzone for controllers.
        horizontalInput = Mathf.Abs(value) < 0.1f ? 0 : value;
    }

    private void UpdateSpeed()
    {
        const float MPH_CONVERSION = 2.23694f;

        // Cache the target.linearVelocity
        Vector3 vel = target.linearVelocity;
        currentSpeed = vel.magnitude * MPH_CONVERSION;

        if (Mathf.Abs(currentSpeed - lastDisplayedSpeed) >= 1f)
        {
            lastDisplayedSpeed = currentSpeed;

            speedText.text = $"{currentSpeed:00} MPH";

            float t = Mathf.Clamp01(currentSpeed / maxSpeed);
            float angle = Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, t);

            needle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(needle.localEulerAngles.z, angle, Time.deltaTime * 10f));
        }
    }

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        wheels[0].wCollider.steerAngle = steeringAngle;
        wheels[1].wCollider.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            float torque = verticalInput * motorForce;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].wCollider.motorTorque = torque;
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].wCollider.motorTorque = 0;
            }
        }
    }

    // Applies the brakes.
    private void ApplyBrake(float amount)
    {
        float torque = brakeForce * amount;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].wCollider.brakeTorque = torque;
        }
        rearLight1.gameObject.SetActive(true);
        rearLight2.gameObject.SetActive(true);
    }

    // Releases the brakes.
    private void ReleaseBrake()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].wCollider.brakeTorque = 0;
        }
        rearLight1.gameObject.SetActive(false);
        rearLight2.gameObject.SetActive(false);
    }

    private void ToggleIndicator(ref bool indicatorState, IEnumerator routineMethod, ref Coroutine indicatorRoutine, Light indicatorLight)
    {
        indicatorState = !indicatorState;

        if (indicatorState)
        {
            if (indicatorRoutine == null)
            {
                indicatorRoutine = StartCoroutine(routineMethod);
            }
            indicatorLight.gameObject.SetActive(true);
            indicating = true;
            pressCount++;
        }
        else
        {
            if (indicatorRoutine != null)
            {
                StopCoroutine(indicatorRoutine);
                indicatorRoutine = null;
            }
            indicatorLight.gameObject.SetActive(false);
            indicating = false;
            pressCount--;
        }
    }

    private void BeginReversing()
    {
        reversing = true;

        for (int i = 0; i < wheels.Length; i++)
        {
            if (!braking)
            {
                wheels[i].wCollider.motorTorque = -motorForce;
            }
        }

        foreach (Light reverser in reverseLight)
        {
            reverser.gameObject.SetActive(true);
        }
    }

    private void StopReversing()
    {
        reversing = false;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].wCollider.motorTorque = 0;
        }

        foreach (Light reverser in reverseLight)
        {
            reverser.gameObject.SetActive(false);
        }
    }

    private void FlipCar()
    {
        Vector3 currentPos = transform.position;
        float yaw = transform.eulerAngles.y;

        transform.position = new Vector3(currentPos.x, currentPos.y + 1f, currentPos.z);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Stops the car from launching into the air.
        target.linearVelocity = Vector3.zero;
        target.angularVelocity = Vector3.zero;
    }

    IEnumerator TurningLeft()
    {
        while (turningLeft)
        {
            leftIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
            leftIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(indicator);
        }
    }

    IEnumerator TurningRight()
    {
        while (turningRight)
        {
            rightIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
            rightIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(indicator);
        }
    }

    private void UpdateWheelPoses()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            UpdateWheelPose(wheels[i]);
        }
    }

    private void UpdateWheelPose(Wheel wheel)
    {
        wheel.wCollider.GetWorldPose(out Vector3 carPos, out Quaternion carQuat);

        wheel.vehicle.position = carPos;
        wheel.vehicle.rotation = carQuat;
    }
}
