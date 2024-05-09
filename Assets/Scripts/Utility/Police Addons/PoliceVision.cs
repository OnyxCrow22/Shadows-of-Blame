using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceVision : MonoBehaviour
{
    public LayerMask player;
    public float rayLength = 30;
    public GameObject policeSystem;
    PoliceLevel policing;
    public bool playerSpotted = false;

    private void Start()
    {
        policeSystem = GameObject.FindGameObjectWithTag("GameManager");
        policing = policeSystem.GetComponent<PoliceLevel>();
    }

    private void FixedUpdate()
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
                policing.spottedPlayer = true;
                policing.PlayerSpotted();
            }
        }
        if (PoliceLevel.policeLevels >= 1 && !playerSpotted && !policing.cancelPursuit)
        {
            StartCoroutine(SearchForPlayer());
        }
    }

    private IEnumerator SearchForPlayer()
    {
        yield return new WaitForSeconds(5);

        if (!playerSpotted)
        {
            policing.spottedPlayer = false;
            policing.LostPlayer();
        }
    }
}
