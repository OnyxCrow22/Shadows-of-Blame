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
    public GameObject Player;
    public GameObject FOV;

    private void Start()
    {
        policeSystem = GameObject.FindGameObjectWithTag("GameManager");
        policing = policeSystem.GetComponent<PoliceLevel>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        VisionCheck();
    }

    void VisionCheck()
    {
        float DistToPlayer = Vector3.Distance(FOV.transform.position, Player.transform.position);

        if (Physics.Raycast(FOV.transform.position, FOV.transform.forward, out RaycastHit playerHit, rayLength) && PoliceLevel.policeLevels >= 1 && DistToPlayer <= 10)
        {
            Debug.DrawRay(FOV.transform.position, FOV.transform.forward, Color.red);
            if (playerHit.collider.CompareTag("Player"))
            {
                playerSpotted = true;
                policing.spottedPlayer = true;
                policing.PlayerSpotted();
            }
        }
        if (PoliceLevel.policeLevels >= 1 && !playerSpotted && !policing.cancelPursuit && DistToPlayer >= 30)
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
