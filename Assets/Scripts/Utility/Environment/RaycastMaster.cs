using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RaycastMaster : MonoBehaviour
{
    [Header("Raycast References")]
    public GameObject interactKey;
    public PlayerMovementSM playsm;

    // Update is called once per frame
    void Update()
    {
        EvidenceCollecting();
        DoorHandling();
    }

    public void DoorHandling()
    {

    }

    public void EvidenceCollecting()
    {
        Ray evidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float rayLength = 4;
        if (Physics.Raycast(evidenceRay, out RaycastHit evidenceHit, rayLength))
        {
            if (evidenceHit.collider.gameObject.tag == "Evidence")
            {
                CollectEvidence collectEvidence = evidenceHit.collider.gameObject.GetComponent<CollectEvidence>();  
                Debug.Log("HIT THE EVIDENCE!");
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && !collectEvidence.reading)
                {
                    collectEvidence.PickUp();
                    collectEvidence.reading = true;
                }
                else if (Input.GetKeyDown(KeyCode.E) && collectEvidence.reading)
                {
                    collectEvidence.CloseWindow();
                }
            }
        }
        else
        {
            interactKey.SetActive(false);
        }
    }
}
