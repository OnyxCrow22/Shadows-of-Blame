using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowWaypoints : MonoBehaviour
{
    public GameObject[] destination;
    public GameObject[] pedestrianDest;
    public GameObject waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    GameObject currentDestinationNode;
    [HideInInspector]
    public GameObject currentPedestrianNode;
    public NavMeshAgent agent;
    Graph g;
    int randomDestIndex;
    int randomPedestrianIndex;

    private void Start()
    {
        waypoints = waypointManager.GetComponent<WaypointManager>().waypoints;
        g = waypointManager.GetComponent<WaypointManager>().graph;
        currentNode = waypoints[0];
        agent = GetComponent<NavMeshAgent>();

        Invoke("AssignRandomDestination", 2);
    }

    private void Awake()
    {
        waypointManager = GameObject.FindGameObjectWithTag("W");
        pedestrianDest = GameObject.FindGameObjectsWithTag("PedDests");
    }

    public void AssignRandomDestination()
    {
      //  randomDestIndex = Random.Range(0, destination.Length);
        randomPedestrianIndex = Random.Range(0, pedestrianDest.Length);
      //  currentDestinationNode = destination[randomDestIndex];
        currentPedestrianNode = pedestrianDest[randomPedestrianIndex];
      //  g.AStar(currentNode, currentDestinationNode);
        g.AStar(currentNode, currentPedestrianNode);
        SetAIDestination();
    }

    public void SetAIDestination()
    {
        agent.isStopped = false;
       // agent.SetDestination(currentDestinationNode.transform.position);
        agent.SetDestination(currentPedestrianNode.transform.position);
    }    
}
