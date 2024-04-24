using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WestralWoes : MonoBehaviour
{
    [Header("Text references")]
    public TextMeshProUGUI objective;
    public TextMeshProUGUI missionName;
    public TextMeshProUGUI subObjective;

    [Header("Boolean references")]
    public bool inSafehouse = true;
    public bool onWestInsbury = false;
    public bool HaliPark = false;
    public bool CollectedEvidenceHPark = false;
    public bool inPrescott = false;
    public bool CollectedEvidencePrescott = false;
    public bool inNorthbyCompound = false;

    [Header("Int references")]
    public int HaliEvidenceCollected = 0;
    public int HaliEvidenceTotal;
    public int PrescottEvidenceCollected = 0;
    public int PrescottEvidenceTotal;
    public int NorthbyGangEliminated = 0;
    public int NorthbyGangAmount;

    [Header("Gameobject references")]
    public GameObject clue;
    public GameObject[] NorthbyGang;

    [Header("Script references")]
    public WWSafehouseCheck safehouseVerify;
    public WWCrossedBridge bridgeCheck;
    public WWHalifaxPark halifaxPark;
    public WWCollectHParkEvidence HParkCollect;
    public WWPrescott prescottCheck;

    private void Start()
    {
        missionName.text = "Westral Woes";

        if (!inSafehouse)
        {
            objective.text = "Go to West Insbury.";
            WesteriaIsland();
        }

        else if (inSafehouse)
        {
            LeaveSTMSafehouse();
            objective.text = "Leave the safehouse.";
        }
    }

    void Update()
    {
        if (PoliceLevel.levelStage >= 1)
        {
            objective.text = "Lose the police.";
        }
    }
    void LeaveSTMSafehouse()
    {
        if (safehouseVerify.hasLeft)
        {
            WesteriaIsland();
        }
    }

    void WesteriaIsland()
    {
        if (bridgeCheck.WestInsbury)
        {
            HalifaxPark();
        }
    }

    void HalifaxPark()
    {
        if (halifaxPark.HPark)
        {
            SearchEvidenceHalifaxPark();
        }
    }

    void SearchEvidenceHalifaxPark()
    {
        if (CollectedEvidenceHPark)
        {
            Prescott();
        }
    }

    void Prescott()
    {
        if (prescottCheck.inPrescott)
        {
            SearchEvidencePrescott();
        }
    }

    void SearchEvidencePrescott()
    {
        if (CollectedEvidencePrescott)
        {
            CompoundInNortby();
        }    
    }

    void CompoundInNortby()
    {
        
    }

    void GangLeaderA()
    {

    }

    void RemainingEnemiesA()
    {

    }

    void TakeEvidenceA()
    {

    }

    void CompoundInNorthBeach()
    {

    }

    void GangLeaderB()
    {

    }

    void RemainingEnemiesB()
    {

    }

    void TakeEvidenceB()
    {

    }

    void LosePolice()
    {

    }

    void GoToSafehouseKensingtonBoulevard()
    {

    }
}
