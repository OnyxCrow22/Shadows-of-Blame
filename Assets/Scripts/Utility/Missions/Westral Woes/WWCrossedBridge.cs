using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWCrossedBridge : MonoBehaviour
{
    public WestralWoes WW;
    public bool WestInsbury = false;

    private void Update()
    {
        if (WestInsbury)
        {
            CheckPolice();
        }
    }
    void CheckPolice()
    {
        if (PoliceLevel.policeLevels >= 1)
        {
            WW.objective.text = "Lose the police.";
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
        }
        if (WW.police.cancelPursuit)
        {
            CheckPlayer();
        }
    }

    void CheckPlayer()
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
            {
                WestInsbury = true;
                WW.onWestInsbury = true;
                WW.objective.text = "Go to Halifax Park.";
                WW.locationClues[0].text = "It's located in the CENTRE of WEST INSBURY.";
                WW.locationClues[1].text = "The Kensington Boulevard skyscrapers are in the northern part of the district.";
                WW.locationClues[2].text = "The park is surrounded by a cluster of tall skyscrapers.";
            }
        }
    }
}
