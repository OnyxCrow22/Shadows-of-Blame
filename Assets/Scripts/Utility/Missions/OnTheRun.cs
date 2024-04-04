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
    public GameObject[] evidence;
    public Transform Compound;
    public Collider WestralSquareCheck;
    public GameObject gangLeader;
    public GameObject[] enemies;
    // public PoliceLevel police;
    public GameObject otrTrigger;
    public TextMeshProUGUI objective;
    public GameObject evidenceWall;
    public int requiredEvidence = 3;

    private void Start()
    {
        inWestralSquare = false;
        canAccessWesteria = false;

        GoToWestralSquare();
    }

    void GoToWestralSquare()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (WestralSquareCheck.bounds.Contains())
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
