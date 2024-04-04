using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AICarController : MonoBehaviour
{
    [Header("Car Settings")]

    [Header("Car References")]
    public NavMeshAgent vehicle;
    public GameObject[] indicators;
    public GameObject rearLight;
    public GameObject reverseLight;

    public bool braking = false;

    public void FixedUpdate()
    {
        Steer();
        Accelerate();
        Brake();
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
        }
        else
        {
        }
    }
}
