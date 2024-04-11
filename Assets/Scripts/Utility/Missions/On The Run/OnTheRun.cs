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
    public bool leftSafehouse, Evidence, GangLeader, Escaped, InCompound, GangEvidence;
    public WestralSquareCheck WSCheck;
    public GangCompoundCheck GCCheck;
    public GangLeaderLogic GLLogic;
   // public GangMemberLogic GMLogic;
   // public GangEvidenceCollect GECollect;
    public Collider PlayerHouseSTMCheck;
    public GameObject clue;
    public GameObject[] enemies;
    // public PoliceLevel police;
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
            inWestralSquare = true;
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
            InCompound = true;
            KillGangLeader();
        }
    }

    void KillGangLeader()
    {
        if (GLLogic.isDead)
        {
            GangLeader = true;
            TakeEvidenceFromGang();
           // if (!GMLogic.enemiesDead)
            {
                KillRemainingEnemies();
            }
           // if (GMLogic.enemiesDead)
            {
                TakeEvidenceFromGang();
            }
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
       // if (GECollect.evidence)
        {
            GangEvidence = true;
            LosePolice();
        }
    }

    void LosePolice()
    {
        objective.text = "Go to your safehouse.";
    }

    void GoToKingstonStreet()
    {
        objective.text = "Place the evidence on the evidence board.";
    }
    
    void PlaceEvidence()
    {
        
    }
}
