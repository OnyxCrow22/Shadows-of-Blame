using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    public float indicator = 0.5f;

    public Rigidbody target;
    public TextMeshProUGUI speedText;
    public Light leftIndicator, rightIndicator;
    public Light rearLight1, rearLight2;
    public Light reverseLight1, reverseLight2;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public bool braking = false;
    public bool turningLeft = false;
    public bool turningRight = false;
    public bool indicating = false;

    public void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        TurnIndicators();
        UpdateWheelPoses();
    }

    private void Update()
    {
        UpdateSpeed();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) && !braking)
        {
            Brake();
            braking = true;
        }

        else if (!Input.GetKey(KeyCode.Space) || !Input.GetKeyDown(KeyCode.S) && currentSpeed > 0 && braking)
        {
            braking = false;
        }

        if (Input.GetKey(KeyCode.S) && !braking && currentSpeed <= 0)
        {
            reverseLight1.gameObject.SetActive(true);
            reverseLight1.gameObject.SetActive(true);
        }
        else if (braking || currentSpeed > 0)
        {
            reverseLight1.gameObject.SetActive(false);
            reverseLight2.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            rearLight1.gameObject.SetActive(true);
            rearLight2.gameObject.SetActive(true);
        }
        else
        {
            rearLight1.gameObject.SetActive(false);
            rearLight2.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            TurnIndicators();
            turningLeft = true;
            indicating = true;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            TurnIndicators();
            turningRight = true;
            indicating = true;
        }

        else 
        {
            turningRight = false;
            indicating = false;
            turningLeft = false;
            
        }
    }

    private void UpdateSpeed()
    {
        currentSpeed = target.velocity.magnitude * 2.23694f;

        speedText.text = currentSpeed.ToString("00" + " MPH");
    }

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = verticalInput * motorForce;
        frontPassengerW.motorTorque = verticalInput * motorForce;
        rearDriverW.motorTorque = verticalInput * motorForce;
        rearPassengerW.motorTorque = verticalInput * motorForce;
    }

    private void Brake()
    {
        if (braking)
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
            rearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
            rearLight1.gameObject.SetActive(true);
            rearLight2.gameObject.SetActive(true);
        }
        else
        {
            frontDriverW.brakeTorque = 0f;
            frontPassengerW.brakeTorque = 0f;
            rearDriverW.brakeTorque = 0f;
            rearPassengerW.brakeTorque = 0f;
            rearLight1.gameObject.SetActive(false);
            rearLight2.gameObject.SetActive(false);
        }
    }

    private void TurnIndicators()
    {
        StartCoroutine(Turning());
    }

    IEnumerator Turning()
    {
        while (true)
        {
            if (turningRight)
            {
                rightIndicator.gameObject.SetActive(true);
                rightIndicator.gameObject.SetActive(false);
                yield return new WaitForSeconds(indicator);
                rightIndicator.gameObject.SetActive(true);
                yield return new WaitForSeconds(indicator);
            }
            else if (turningLeft)
            {
                leftIndicator.gameObject.SetActive(true);
                yield return new WaitForSeconds(indicator);
                leftIndicator.gameObject.SetActive(false);
                yield return new WaitForSeconds(indicator);
                leftIndicator.gameObject.SetActive(true);
            }
        
            if (!indicating)
            {
                yield break;
            }
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider wCol, Transform vtransform)
    {
        Vector3 carPos;
        Quaternion carQuat;

        wCol.GetWorldPose(out carPos, out carQuat);

        vtransform.transform.position = carPos;
        vtransform.transform.rotation = carQuat;
    }
}
