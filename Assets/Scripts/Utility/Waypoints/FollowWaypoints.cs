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
        currentDestinationNode = waypoints[0];
        agent = GetComponent<NavMeshAgent>();

        Invoke("AssignRandomDestination", 2);
    }

    private void Awake()
    {
        waypointManager = GameObject.FindGameObjectWithTag("W");

        if (agent.CompareTag("FemaleNPC") || agent.CompareTag("MaleNPC") || agent.CompareTag("Police"))
        {
            pedestrianDest = GameObject.FindGameObjectsWithTag("PedDests");
        }
    }

    public void AssignRandomDestination()
    {
        if (agent.CompareTag("Vehicle"))
        {
            randomDestIndex = Random.Range(0, destination.Length);
            currentDestinationNode = destination[randomDestIndex];
            g.AStar(currentNode, currentDestinationNode);
            SetAIDestination();
        }
        else if (agent.CompareTag("FemaleNPC") || agent.CompareTag("MaleNPC") || agent.CompareTag("Police"))
        {
            randomPedestrianIndex = Random.Range(0, pedestrianDest.Length);
            currentPedestrianNode = pedestrianDest[randomPedestrianIndex];
            g.AStar(currentNode, currentPedestrianNode);
            SetAIDestination();
        }
    }

    public void SetAIDestination()
    {
        agent.isStopped = false;

        if (agent.CompareTag("Vehicle"))
        {
            agent.SetDestination(currentDestinationNode.transform.position);
        }
        else if (agent.CompareTag("FemaleNPC") || agent.CompareTag("MaleNPC") || agent.CompareTag("Police"))
        {
            agent.SetDestination(currentPedestrianNode.transform.position);
        }
    }    
}
