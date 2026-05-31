using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoAStarWaypoint : MonoBehaviour
{
    public AICarController car;
    public Transform[] waypoints;
    int destinations;
    private void Update()
    {
        if (!car.vehicle.pathPending && car.vehicle.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }

        void GoToNextPoint()
        {
            // End of path
            if (waypoints.Length == 0)
            {
                return;
            }
            car.vehicle.destination = waypoints[destinations].position;
            destinations = (destinations + 1) % waypoints.Length;
        }
    }
}
