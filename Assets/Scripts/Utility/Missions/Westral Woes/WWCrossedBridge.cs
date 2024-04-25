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
            WW.locationClues[0].text = "It's located in the CENTRE of WEST INSBURY.";
            WW.locationClues[1].text = "It's accessible by the M150.";
            WW.locationClues[2].text = "The park is surrounded by tall skyscrapers.";
        }
    }
}
