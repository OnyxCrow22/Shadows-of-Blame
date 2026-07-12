using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Graph graph = new Graph();
    public float connectionDistance = 10f; // Max distance to auto-link nodes

    void Start()
    {
        // 1. Find nodes by tag, not by manual inspector arrays
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Waypoint");

        // 2. Add all found nodes to graph
        foreach (GameObject node in nodes)
        {
            graph.AddNode(node);
        }

        // 3. Auto-link nodes based on proximity
        foreach (GameObject nodeA in nodes)
        {
            foreach (GameObject nodeB in nodes)
            {
                if (nodeA != nodeB && Vector3.Distance(nodeA.transform.position, nodeB.transform.position) <= connectionDistance)
                {
                    graph.AddEdge(nodeA, nodeB);
                }
            }
        }
    }
}