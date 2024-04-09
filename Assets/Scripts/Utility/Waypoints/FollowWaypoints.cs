using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowWaypoints : MonoBehaviour
{
    GameObject[] destination;
    public GameObject waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    GameObject currentDestinationNode;
    public NavMeshAgent vehicle;
    Graph g;
    int randomIndex;
    int randomDestIndex;

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
        destination = GameObject.FindGameObjectsWithTag("Destination");
        randomDestIndex = Random.Range(0, destination.Length);
        currentDestinationNode = destination[randomDestIndex];
        g.AStar(currentNode, currentDestinationNode);
        SetAIDestination();
    }

    public void SetAIDestination()
    {
        vehicle.isStopped = false;
        vehicle.SetDestination(currentDestinationNode.transform.position);
    }    
}
