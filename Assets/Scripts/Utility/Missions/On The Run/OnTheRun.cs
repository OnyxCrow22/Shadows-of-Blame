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
    public bool leftSafehouse, Evidence, EliminatedGang, Escaped, InCompound, GangEvidence;
    public bool missionComplete = false;
    public WestralSquareCheck WSCheck;
    public GangCompoundCheck GCCheck;
    public GangLeaderLogic GLLogic;
   // public GangMemberLogic GMLogic;
    public GangEvidenceCollect GECollect;
    public PoliceCheck pCheck;
    public SafehouseCheck sCheck;
    public EvidencePlace pEvidence;
    public Collider PlayerHouseSTMCheck;
    public GameObject clue;
    public GameObject[] enemies;
    public PoliceLevel police;
    public TextMeshProUGUI objective, subObjective;
    public TextMeshProUGUI mission;
    public GameObject evidenceWall;
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

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (PlayerHouseSTMCheck.bounds.Contains(playerPos) && leftSafehouse == false)
        {
            LeaveSafehouse();
            inSafehouse = true;
        }
        else
        {
            GoToWestralSquare();
            objective.text = "Go to Westral Square.";
            inSafehouse = false;
        }
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
        objective.text = "Leave the safehouse.";

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (!PlayerHouseSTMCheck.bounds.Contains(playerPos))
        {
            objective.text = "Go to Westral Square.";
            inSafehouse = false;
            leftSafehouse = true;
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
        if(GangEvidenceCollect.evidence && PoliceLevel.levelStage == 0)
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
            canAccessWesteria = true;
            missionComplete = true;
        }
    }
}
