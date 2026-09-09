using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    [Header("Node References")]
    public List<Edge> edgeList = new List<Edge>();
    [SerializeReference] public Node path;
    GameObject ID;

    public float f, g, h;
    [SerializeReference] public Node cameFrom;

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
