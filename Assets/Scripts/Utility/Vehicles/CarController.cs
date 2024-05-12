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
    public Light[] reverseLight;

    [Header("Wheel colliders")]
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    [Header("Transforms")]
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    [Header("Booleans")]
    public bool braking = false;
    public bool turningLeft = false;
    public bool turningRight = false;
    public bool indicating = false;
    public bool reversing = false;

    [Header("Float references")]
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI Elements")]
    public RectTransform needle;
    public GameObject speedometer;

    public void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Reverse();
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


        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) && verticalInput <= 0 && !braking)
        {
            Brake();
            braking = true;
        }

        else if (verticalInput > 0.01f)
        {
            braking = false;
        }

        if (Input.GetKey(KeyCode.S) && braking && verticalInput <= 0.01f)
        {
            Reverse();
            reversing = true;
            braking = false;
        }

        if (Input.GetKey(KeyCode.LeftBracket))
        {
            TurnIndicators();
            turningLeft = true;
            indicating = true;
        }

        if (Input.GetKey(KeyCode.RightBracket))
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
        const float MPH_CONVERSION = 2.23694f;
        currentSpeed = target.velocity.magnitude * MPH_CONVERSION;

        speedText.text = currentSpeed.ToString("00" + " MPH");
        needle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, currentSpeed / maxSpeed));
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
        if (braking && currentSpeed >= 0)
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
            rearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
            rearLight1.gameObject.SetActive(true);
            rearLight2.gameObject.SetActive(true);
        }
        else if (horizontalInput > 0.01f | verticalInput > 0.01f) 
        {
            frontDriverW.brakeTorque = 0f;
            frontPassengerW.brakeTorque = 0f;
            rearDriverW.brakeTorque = 0f;
            rearPassengerW.brakeTorque = 0f;
            rearLight1.gameObject.SetActive(false);
            rearLight2.gameObject.SetActive(false);
        }
    }

    private void Reverse()
    {
        if (reversing && currentSpeed <= 0)
        {
            frontDriverW.motorTorque = verticalInput * motorForce;
            frontPassengerW.motorTorque = verticalInput * motorForce;
            rearDriverW.motorTorque = verticalInput * motorForce;
            rearPassengerW.motorTorque = verticalInput * motorForce;

            reverseLight[0].gameObject.SetActive(true);
            reverseLight[1].gameObject.SetActive(true);
        }
        // No longer reversing, turn off lights.
        else if (verticalInput > 0.01f | horizontalInput > 0.01f) 
        {
            reverseLight[0].gameObject.SetActive(false);
            reverseLight[1].gameObject.SetActive(false);
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
                yield return new WaitForSeconds(indicator);
                rightIndicator.gameObject.SetActive(false);
                yield return new WaitForSeconds(indicator);
                rightIndicator.gameObject.SetActive(true);
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
