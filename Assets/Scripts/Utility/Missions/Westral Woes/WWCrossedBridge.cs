using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWCrossedBridge : MonoBehaviour
{
    public WestralWoes WW;
    public bool WestInsbury = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WestInsbury = true;
            WW.onWestInsbury = true;
            WW.objective.text = "Go to Halifax Park.";
        }
    }
}
