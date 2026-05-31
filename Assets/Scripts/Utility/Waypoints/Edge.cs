using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    [Header("Edge References")]
    public Node startNode, endNode;

    public Edge(Node from, Node to)
    {
        startNode = from;
        endNode = to;
    }
}
