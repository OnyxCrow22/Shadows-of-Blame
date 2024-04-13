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
        Accelerate();
        Brake();
        UpdateWheelRotations();
        CheckPlayer();
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

        if (Physics.Raycast(watchRay, out RaycastHit playerHit, range, isPlayer) || Physics.Raycast(watchRay, out RaycastHit NPChit, range, isNPC))
        {
            if (playerHit.collider.CompareTag("Player"))
            {
                // Stop the car, the player is crazy!
                vehicle.isStopped = true;
            }
        }
        else
        {
            // Player not in front of car
            vehicle.isStopped = false;
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
