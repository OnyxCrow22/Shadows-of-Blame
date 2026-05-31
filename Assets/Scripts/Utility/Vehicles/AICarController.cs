using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AICarController : MonoBehaviour
{
    [Header("Car References")]
    public NavMeshAgent vehicle;
    public GameObject[] indicators;
    public GameObject[] rearLight;
    public GameObject[] reverseLight;
    public GameObject carFOV;

    public GameObject player;
    public LayerMask isPlayer, isNPC;

    public Transform frontDriverT, frontPassengerT, rearDriverT, rearPassengerT;

    public bool braking = false;

    public void FixedUpdate()
    {
        Steer();
        Brake();
        UpdateWheelRotations();
        CheckPlayer();
        CheckTrafficLight();
    }

    private void Steer()
    {
        if (vehicle.autoBraking)
        {
            Brake();
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

    void CheckPlayer()
    {
        float range = 20;

        Ray watchRay = new Ray(carFOV.transform.position, Vector3.forward);
        Debug.DrawRay(carFOV.transform.position, carFOV.transform.forward);
        RaycastHit objectHit;

        if (Physics.Raycast(watchRay, out objectHit, range))
        {
            if (objectHit.collider.CompareTag("Player") || (objectHit.collider.CompareTag("FemaleNPC") || (objectHit.collider.CompareTag("MaleNPC"))))
            {
                // Stop the car, the player/NPC is crazy!
                vehicle.isStopped = true;
            }
        }
        else
        {
            // Player not in front of car
            vehicle.isStopped = false;
        }
    }

    void CheckTrafficLight()
    {
        float range = 20;

        Ray watchRay = new Ray(carFOV.transform.position, Vector3.forward);
        Debug.DrawRay(carFOV.transform.position, carFOV.transform.forward);
        RaycastHit objectHit;

        if (Physics.Raycast(watchRay, out objectHit, range))
        {
            if (objectHit.collider.CompareTag("TrafficLightChecker"))
            {
                TrafficLightCheck checking = objectHit.collider.GetComponent<TrafficLightCheck>();

                if (checking.lights.red == true || checking.lights.amber == true)
                {
                    // Light is red or amber, stop the car.
                    vehicle.isStopped = true;
                }
                else if (checking.lights.green == true)
                {
                    // Player not in front of car
                    vehicle.isStopped = false;
                }
            }
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
