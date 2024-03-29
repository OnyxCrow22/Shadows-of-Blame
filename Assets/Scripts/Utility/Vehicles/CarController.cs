using System.Collections;
using System.Collections.Generic;
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

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public bool braking = false;

    public void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
    }

    public void GetInput()
    {
        horizontalInput = -Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.S)) && !braking)
        {
            Brake();
        }
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
        }
        else
        {
            frontDriverW.brakeTorque = 0f;
            frontPassengerW.brakeTorque = 0f;
            rearDriverW.brakeTorque = 0f;
            rearPassengerW.brakeTorque = 0f;
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
