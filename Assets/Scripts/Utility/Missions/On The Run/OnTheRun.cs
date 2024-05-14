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
    public bool gangLeaderdead = false;
    public bool allenemiesKilled = false;

    [Header("Script references")]
    public WestralSquareCheck WSCheck;
    public GangCompoundCheck GCCheck;
    public GangLeaderLogic GLLogic;
    public StartSafehouse sSafehouse;
    public GangEvidenceCollect GECollect;
    public WesteriaAccessibility wAccess;
    public WesteriaLocked wLocked;
    public SafehouseCheck sCheck;
    public EvidencePlace pEvidence;
    public PoliceLevel police;
    public WestralWoes WW;

    [Header("Gameobject References")]
    public GameObject clue;
    public GameObject objectiveHolder;
    public GameObject[] enemies;
    public GameObject missionFailed;
    public GameObject evidenceWall;
    public GameObject westeriaUnlocked;
    public GameObject warningHolder, dangerPanel;
    public GameObject player;
    public GameObject OTRHolder;
    public GameObject WWHolder;

    [Header("Text references")]
    public TextMeshProUGUI objective, subObjective;
    public TextMeshProUGUI mission;
    public TextMeshProUGUI warningText, dangerText;
    public TextMeshProUGUI failText;

    [Header("Int references")]
    public int requiredEvidence = 3;
    public int totalEvidence = 3;
    public int collectedEvidence = 0;
    public int gangMemberCount = 5;
    public int gangMembersKilled = 0;

    private void Start()
    {
        inWestralSquare = false;
        canAccessWesteria = false;
        mission.text = "On The Run";

        if (leftSafehouse)
        {
            objective.text = "Go To Westral Square.";
            GoToWestralSquare();
        }
        if (!leftSafehouse)
        {
            objective.text = "Leave the safehouse.";
            LeaveSafehouse();
        }
    }

    private void Update()
    {
        if (player.GetComponent<PlayerMovementSM>().health.health == 0)
        {
            Time.timeScale = 0;
            missionFailed.SetActive(false);
            failText.text = "Harrison died";
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
        else if (GLLogic.isDead && !EliminatedGang && !allenemiesKilled)
        {
            KillRemainingEnemies();
        }
    }

    void KillRemainingEnemies()
    {
        if (EliminatedGang && GLLogic.isDead && allenemiesKilled)
        {
            TakeEvidenceFromGang();
        }
    }

    void TakeEvidenceFromGang()
    {
        if (GangEvidence)
        {
            LosePolice();
        }
    }

    void LosePolice()
    {
        if(GangEvidence && PoliceLevel.policeLevels == 0)
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
            GetComponent<OnTheRun>().enabled = false;
            WW.GetComponent<WestralWoes>().enabled = true;
        }
    }
}
