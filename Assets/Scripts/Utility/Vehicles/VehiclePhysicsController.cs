using UnityEngine;

public class VehiclePhysicsController : MonoBehaviour
{
    public WheelCollider[] driveWheels;
    public WheelCollider[] steerWheels;

    public float motorTorque = 1500f;
    public float brakeTorque = 3000f;
    public float maxSteerAngle = 30f;

    public void Accelerate(float throttle)
    {
        foreach (var wheel in driveWheels)
            wheel.motorTorque = throttle * motorTorque;
    }

    public void Brake(float brake)
    {
        foreach (var wheel in driveWheels)
            wheel.brakeTorque = brake * brakeTorque;
    }

    public void Steer(float steerInput)
    {
        foreach (var wheel in steerWheels)
            wheel.steerAngle = steerInput * maxSteerAngle;
    }
}
