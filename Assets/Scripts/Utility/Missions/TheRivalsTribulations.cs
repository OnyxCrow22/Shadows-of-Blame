using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheRivalsTribulations : MonoBehaviour
{
    public TextMeshProUGUI objective;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PoliceLevel.levelStage >= 1)
        {
            objective.text = "Lose the police";
        }
    }

    void LeaveKensingtonBlvdSafehouse()
    {

    }

    void GoToDistrict()
    {

    }

    void TailCar()
    {

    }

    void SearchCompound()
    {

    }

    void LeaveCompound()
    {

    }

    void StopGangLeader()
    {

    }

    void TakeEvidence()
    {

    }

    void LosePolice()
    {

    }

    void GoToKingstonStreet()
    {

    }

    void WaitUntilDusk()
    {

    }

    void ConstructionSite()
    {

    }

    void FloorNumber()
    {

    }

    void ChaseRival()
    {

    }

    void KillRival()
    {
        
    }
}
