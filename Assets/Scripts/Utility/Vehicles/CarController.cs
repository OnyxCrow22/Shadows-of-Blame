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

    [Header("Wheels")]
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    [Header("Car transforms")]
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public bool braking = false;

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !braking)
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
    }

    private void Brake()
    {
        frontDriverW.brakeTorque = brakeForce;
        frontPassengerW.brakeTorque = brakeForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider wCol, Transform vTransform)
    {
        Vector3 position = vTransform.position;
        Quaternion quaternion = vTransform.rotation;

        wCol.GetWorldPose(out position, out quaternion);
    }
}
