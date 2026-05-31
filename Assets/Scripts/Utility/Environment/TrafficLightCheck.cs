using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficLightCheck : MonoBehaviour
{
    public TrafficLight lights;
    GameObject currentCar;
    GameObject currentNPC;
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

        if (other.CompareTag("MaleNPC") || other.CompareTag("FemaleNPC"))
        {
            NPCMovementSM NPC = currentNPC.GetComponent<NPCMovementSM>();
            if (NPC != null)
            {
                if (lights.red == true)
                {
                    NPC.NPC.isStopped = false;
                }
                else if (lights.green == true || lights.amber == true)
                {
                    NPC.NPC.isStopped = true;
                }
            }
        }
    }
}
