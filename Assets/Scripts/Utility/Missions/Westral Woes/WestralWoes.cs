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
    public TextMeshProUGUI[] locationClues;

    [Header("Boolean references")]
    public bool inSafehouse = true;
    public bool onWestInsbury = false;
    public bool HaliPark = false;
    public bool CollectedEvidenceHPark = false;
    public bool inPrescott = false;
    public bool CollectedEvidencePrescott = false;
    public bool inNorthbyCompound = false;
    public bool northbyLeaderdown = false;
    public bool allNorthby = false;
    public bool collectedNorthbyEvidence = false;
    public bool enteredNorthBeach = false;
    public bool inNorthBeachcompound = false;
    public bool allNorthBeachGangsters = false;
    public bool collectedNorthBeachEvidence = false;

    [Header("Int references")]
    public int HaliEvidenceCollected = 0;
    public int HaliEvidenceTotal;
    public int PrescottEvidenceCollected = 0;
    public int PrescottEvidenceTotal;
    public int NorthbyGangEliminated = 0;
    public int NorthbyGangAmount;
    public int NorthBeachGangEliminated = 0;
    public int NorthBeachGangAmount;

    [Header("Gameobject references")]
    public GameObject clue;
    public GameObject HParkHolder;
    public GameObject PrescottHolder;
    public GameObject[] NorthbyGang;
    public GameObject[] NorthBeachGang;
    public GameObject[] NorthBeachEvidence;
    public GameObject LocationClueHolder;

    [Header("Script references")]
    public WWSafehouseCheck safehouseVerify;
    public WWCrossedBridge bridgeCheck;
    public WWHalifaxPark halifaxPark;
    public WWPrescott prescottCheck;
    public WWNorthBeachCheck northBeach;

    public void Start()
    {
        this.GetComponent<WestralWoes>().enabled = true;
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
            CompoundInNorthby();
        }    
    }

    void CompoundInNorthby()
    {
        if (inNorthbyCompound)
        {
            GangLeaderA();
        }
    }

    void GangLeaderA()
    {
        if (NorthbyGang.Length > 0 && northbyLeaderdown)
        {
            RemainingEnemiesA();
        }
        if (NorthbyGang.Length <= 0)
        {
            TakeEvidenceA();
        }
    }

    void RemainingEnemiesA()
    {
        if (allNorthby && northbyLeaderdown)
        {
            TakeEvidenceA();
        }
    }

    void TakeEvidenceA()
    {
        if (collectedNorthbyEvidence)
        {
            NorthBeach();
        }
    }

    void NorthBeach()
    {
        if (northBeach.enteredNorthBeach)
        {
            CompoundInNorthBeach();
        }
    }

    void CompoundInNorthBeach()
    {
        if (inNorthBeachcompound)
        {
            NorthBeachGangsters();
        }
    }

    void NorthBeachGangsters()
    {
        if (allNorthBeachGangsters)
        {
            TakeEvidenceB();
        }
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
