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
    public float indicator = 0.5f;

    public Rigidbody target;
    public TextMeshProUGUI speedText;
    public GameObject leftIndicator, rightIndicator;
    public GameObject rearLight;
    public GameObject reverseLight;

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
        verticalInput = -Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.S)) && !braking)
        {
            Brake();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            reverseLight.SetActive(true);
        }
        else
        {
            reverseLight.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.W)))
        {
            rearLight.SetActive(true);
        }
        else
        {
            rearLight.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            TurnIndicators();
            turningLeft = true;
            indicating = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            TurnIndicators();
            turningRight = true;
            indicating = true;
        }
    }

    private void UpdateSpeed()
    {
        float currentSpeed = target.velocity.magnitude * 2.23694f;

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
            rearLight.SetActive(true);
        }
        else
        {
            frontDriverW.brakeTorque = 0f;
            frontPassengerW.brakeTorque = 0f;
            rearDriverW.brakeTorque = 0f;
            rearPassengerW.brakeTorque = 0f;
            rearLight.SetActive(false);
        }
    }

    private void TurnIndicators()
    {
        StartCoroutine(Turning());
    }

    IEnumerator Turning()
    {
        while (indicating)
        if (turningRight)
        {
            rightIndicator.SetActive(true);
            yield return new WaitForSeconds(indicator);
            rightIndicator.SetActive(false);
        }
        else if (turningLeft)
        {
            leftIndicator.SetActive(true);
            yield return new WaitForSeconds(indicator);
            leftIndicator.SetActive(false);
        }
        
        if (!indicating)
        {
            yield break;
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
