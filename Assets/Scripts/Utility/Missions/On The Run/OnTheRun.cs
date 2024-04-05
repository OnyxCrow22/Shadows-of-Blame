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
    public bool leftSafehouse, Evidence, GangLeader, Escaped;
    public GameObject[] evidence;
    public Transform Compound;
    public WestralSquareCheck WSCheck;
    public Collider PlayerHouseSTMCheck;
    public GameObject gangLeader;
    public GameObject[] enemies;
    // public PoliceLevel police;
    public GameObject otrTrigger;
    public TextMeshProUGUI objective;
    public TextMeshProUGUI mission;
    public GameObject evidenceWall;
    public int requiredEvidence = 3;
    public int totalEvidence = 3;
    public int collectedEvidence = 0;

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

    }

    void ReadEvidence()
    {

    }

    void GoToCompound()
    {

    }

    void KillGangLeader()
    {

    }

    void KillRemainingEnemies()
    {

    }

    void TakeEvidenceFromGang()
    {

    }

    void LosePolice()
    {

    }

    void GoToKingstonStreet()
    {

    }
    
    void PlaceEvidence()
    {
        
    }
}
