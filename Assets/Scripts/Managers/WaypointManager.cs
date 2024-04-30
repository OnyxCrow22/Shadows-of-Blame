
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { ONEWAY, TWOWAY, TURNLEFT, TURNRIGHT, SLIP, NOTURNLEFT, NOTURNRIGHT }
    public GameObject lastNode, nextNode;
    public direction dirCheck;
}

public class WaypointManager : MonoBehaviour
{
    [Header("Waypoint References")]
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoints in waypoints)
            {
                graph.AddNode(waypoints);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.lastNode, l.nextNode);
                if (l.dirCheck == Link.direction.TWOWAY)
                {
                    graph.AddEdge(l.lastNode, l.nextNode);
                }
            }
        }
    }
}
