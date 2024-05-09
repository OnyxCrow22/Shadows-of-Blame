using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceVision : MonoBehaviour
{
    public LayerMask player;
    public float rayLength = 30;
    public GameObject policeSystem;
    public bool playerSpotted = false;

    private void Start()
    {
        policeSystem = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void Update()
    {
        VisionCheck();
    }

    void VisionCheck()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit playerHit, rayLength) && PoliceLevel.policeLevels >= 1)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (playerHit.collider.CompareTag("Player"))
            {
                playerSpotted = true;
                policeSystem.GetComponent<PoliceLevel>().spottedPlayer = true;
                policeSystem.GetComponent<PoliceLevel>().PlayerSpotted();
            }
        }
        else
        {
            policeSystem.GetComponent<PoliceLevel>().spottedPlayer = false;
            playerSpotted = false;
        }
    }
}
