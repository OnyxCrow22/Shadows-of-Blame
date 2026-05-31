using UnityEngine;

public class WWCompoundNorthBeach : MonoBehaviour
{
    public WestralWoes WW;
    public bool inCompound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !WW.collectedNorthBeachEvidence)
        {
            inCompound = true;
            WW.inNorthBeachcompound = true;
            WW.objective.text = "Kill the gang members: " + WW.NorthBeachGangEliminated + " / " + WW.NorthBeachGangAmount;
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!inCompound && !WW.allNorthBeachGangsters && !WW.collectedNorthBeachEvidence)
        {
            inCompound = false;
            WW.objective.text = "Go back to Palm Surf.";
        }
    }
}
