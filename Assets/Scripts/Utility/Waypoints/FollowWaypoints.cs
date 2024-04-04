using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowWaypoints : MonoBehaviour
{
    public Vector3 destination;
    public GameObject waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWaypoint = 0;
    public NavMeshAgent vehicle;
    Graph g;
    int randomIndex;

    private void Start()
    {
        waypoints = waypointManager.GetComponent<WaypointManager>().waypoints;
        g = waypointManager.GetComponent<WaypointManager>().graph;
        currentNode = waypoints[0];
        vehicle = GetComponent<NavMeshAgent>();

        Invoke("AssignRandomDestination", 2);
    }

    void AssignRandomDestination()
    {
        randomIndex = Random.Range(0, waypoints.Length);
        g.AStar(currentNode, waypoints[randomIndex]);
        destination = waypoints[randomIndex].transform.position;
        SetAIDestination();
    }

    public void SetAIDestination()
    {
        currentWaypoint = 0;
        vehicle.isStopped = false;
        vehicle.SetDestination(destination);
    }    
}
