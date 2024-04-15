using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTheRun : MonoBehaviour
{
    public Transform Player;
    [Header("Booleans")]
    public bool inWestralSquare = false;
    public bool canAccessWesteria = false;
    public bool inSafehouse = false;
    public bool leftSafehouse, Evidence, EliminatedGang, Escaped, InCompound, GangEvidence, PlacedEvidence, Safehouse;
    public bool missionComplete = false;
    public WestralSquareCheck WSCheck;
    public GangCompoundCheck GCCheck;
    public GangLeaderLogic GLLogic;
    // public GangMemberLogic GMLogic;
    public StartSafehouse sSafehouse;
    public GangEvidenceCollect GECollect;
    public WesteriaAccessibility wAccess;
    public WesteriaLocked wLocked;
    public SafehouseCheck sCheck;
    public EvidencePlace pEvidence;
    public GameObject clue;
    public GameObject objectiveHolder;
    public GameObject[] enemies;
    public PoliceLevel police;
    public TextMeshProUGUI objective, subObjective;
    public TextMeshProUGUI mission;
    public TextMeshProUGUI warningText, dangerText;
    public GameObject evidenceWall;
    public GameObject westeriaUnlocked;
    public GameObject warningHolder, dangerPanel;
    public int requiredEvidence = 3;
    public int totalEvidence = 3;
    public int collectedEvidence = 0;

    public int gangMemberCount = 5;
    public int gangMembersKilled = 0;

    private void Start()
    {
        inWestralSquare = false;
        canAccessWesteria = false;
        inSafehouse = true;
        mission.text = "On The Run";

        objective.text = "Leave the safehouse.";
        LeaveSafehouse();
    }

    private void Update()
    {
        if (PoliceLevel.levelStage >= 1)
        {
            objective.text = "Lose the police.";
        }
    }

    void LeaveSafehouse()
    {
        if (sSafehouse.left)
        {
            GoToWestralSquare();
        }
    }

    void GoToWestralSquare()
    {
        if (WSCheck.WSquare)
        {
            FindEvidenceinWS();
        }
    }   

    void FindEvidenceinWS()
    {
        if (Evidence == true)
        {
            GoToCompound();
        }
    }

    void GoToCompound()
    {
        if (GCCheck.arrivedAtCompound)
        {
            KillGangLeader();
        }
    }

    void KillGangLeader()
    {
        if (GLLogic.isDead)
        {
            TakeEvidenceFromGang();
        }    
    }

    void KillRemainingEnemies()
    {
      //  if (GMLogic.enemiesDead)
        {
            TakeEvidenceFromGang();
        }
    }

    void TakeEvidenceFromGang()
    {
        if (GangEvidenceCollect.evidence)
        {
            LosePolice();
        }
    }

    void LosePolice()
    {
        if(GangEvidenceCollect.evidence && Escaped)
        {
            GoToKingstonStreet();
        }
    }

    void GoToKingstonStreet()
    {
        if (sCheck.inSafehouse)
        {
            PlaceEvidence();
        }
    }
    
    void PlaceEvidence()
    {
        if (pEvidence.EvidencePlaced)
        {
            this.gameObject.SetActive(false);
        }
    }
}
