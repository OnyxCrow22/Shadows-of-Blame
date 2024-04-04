using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    [Header("Node References")]
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    GameObject ID;

    public float f, g, h;
    public Node cameFrom;

    public Node(GameObject i)
    {
        ID = i;
        path = null;
    }

    public GameObject getID()
    {
        return ID;
    }
}
