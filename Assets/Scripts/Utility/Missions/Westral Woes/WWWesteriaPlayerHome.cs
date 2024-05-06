using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWWesteriaPlayerHome : MonoBehaviour
{
    public bool nowHome = false;
    public WestralWoes WW;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
        {
            nowHome = true;
            WW.backHome = true;
            WW.objective.text = "Head upstairs to the North Beach Suite";
            WW.locationClues[0].text = "It's on the 21st Floor.";
            WW.locationClues[0].text = "The lift in the lobby goes to the top floor.";
        }
    }
}
