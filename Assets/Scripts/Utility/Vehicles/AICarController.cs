using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AICarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float topSpeed;

    [Header("Car References")]
    public NavMeshAgent vehicle;
    public GameObject[] indicators;
    public GameObject[] rearLight;
    public GameObject[] reverseLight;

    public Transform frontDriverT, frontPassengerT, rearDriverT, rearPassengerT;

    public bool braking = false;

    public void FixedUpdate()
    {
        Steer();
        Accelerate();
        Brake();
        UpdateWheelRotations();
    }

    private void Steer()
    {
        if (vehicle.autoBraking)
        {
            Brake();
        }
    }

    private void Accelerate()
    {
        float speed = vehicle.velocity.magnitude;
        if (speed < topSpeed && !braking)
        {
            vehicle.Move(transform.forward * topSpeed * Time.deltaTime);
            vehicle.speed += 1;
        }
    }

    private void Brake()
    {
        if (braking)
        {
            vehicle.SetDestination(transform.position);
            rearLight[rearLight.Length].SetActive(true);
        }
    }

    private void UpdateWheelRotations()
    {
        float rotationAngle = -vehicle.velocity.magnitude / ( 2 * Mathf.PI * 0.5f) * 360 * Time.deltaTime;

        RotateWheel(frontDriverT, rotationAngle);
        RotateWheel(frontPassengerT, rotationAngle);
        RotateWheel(rearDriverT, rotationAngle);
        RotateWheel(rearPassengerT, rotationAngle);
    }

    private void RotateWheel(Transform wTransform, float rotationAngle)
    {
        wTransform.Rotate(Vector3.right, rotationAngle, Space.Self);
    }
}
