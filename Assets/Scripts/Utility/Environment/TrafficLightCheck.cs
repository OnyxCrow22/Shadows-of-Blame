using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficLightCheck : MonoBehaviour
{
    public TrafficLight lights;
    GameObject currentCar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            AICarController stopCheck = currentCar.GetComponent<AICarController>();
            if (currentCar != null)
            {
                if (lights.red == true || lights.amber == true)
                {
                    stopCheck.vehicle.isStopped = true;
                }
                else if (lights.green)
                {
                    stopCheck.vehicle.isStopped = false;
                }
            }
        }
    }
}
