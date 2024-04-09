using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectEvidence : MonoBehaviour
{
    public GameObject evidence;
    public GameObject panel;
    public GameObject clueText;
    public bool reading = false;
    public Transform interactTransform;
    public OnTheRun OTR;
    public float interactRange;

    private void Update()
    {
        PickUp();
    }

    private void PickUp()
    {
        if(Input.GetKeyDown(KeyCode.E) && !reading)
        {
            Ray eRay = new Ray(interactTransform.position, interactTransform.forward);
            if (Physics.Raycast(eRay, out RaycastHit evidenceHit, interactRange))
            {
                if (evidenceHit.collider.gameObject.CompareTag("Evidence"))
                {
                    panel.SetActive(true);
                    reading = true;

                    if (Input.GetKeyDown(KeyCode.E) && reading)
                    {
                        OTR.collectedEvidence += 1;
                        panel.SetActive(false);
                        reading = false;
                        clueText.SetActive(true);
                        OTR.clue.SetActive(true);
                        evidence.SetActive(false);
                    }
                }
            }    
        }

    }
}
