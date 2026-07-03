using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastMaster : MonoBehaviour
{
    [Header("Raycast References")]
    public GameObject interactKey;
    public PlayerMovementSM playsm;
    public OnTheRun OTR;
    public WestralWoes WW;
    public VehicleEnterExit vehicular;

    [Header("Cameras")]
    public GameObject playerCamera;
    public GameObject ThirdPersonCamera;

    private bool interactPressed = false;
    public bool evidence = false;
    public bool carDoor = false;
    public bool board = false;
    public bool buttonPressed = false;
    public bool inLift = false;

    private IInteractable lastIntercable = null;


    // Update is called once per frame
    void Update()
    {
        // The master control for interactions.
        HandleInteraction();

        PlaceEvidenceOnBoard();
        interactPressed = false;
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
           interactPressed = true;
           Debug.Log("Door opening..");
        }
    }

    public void HParkEvidenceCollect()
    {
        Ray evidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float rayLength = 4;
        if (Physics.Raycast(evidenceRay, out RaycastHit evidenceHit, rayLength))
        {
            if (evidenceHit.collider.gameObject.tag == "HParkEvidence")
            {
                WWCollectHParkEvidence HParkEvidence = evidenceHit.collider.gameObject.GetComponent<WWCollectHParkEvidence>();
                Debug.Log("HIT THE EVIDENCE!");
                interactKey.SetActive(true);
                if (interactPressed && !HParkEvidence.reading)
                {
                    HParkEvidence.PickUp();
                    HParkEvidence.reading = true;
                }
                else if (interactPressed && HParkEvidence.reading)
                {
                    HParkEvidence.CloseWindow();
                }
            }
        }
    }

    public void PrescottEvidenceCollect()
    {
        Ray evidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float rayLength = 4;
        if (Physics.Raycast(evidenceRay, out RaycastHit evidenceHit, rayLength))
        {
            if (evidenceHit.collider.gameObject.tag == "PrescottEvidence")
            {
                WWCollectPrescottEvidence prescottEvidence = evidenceHit.collider.gameObject.GetComponent<WWCollectPrescottEvidence>();
                Debug.Log("HIT THE EVIDENCE!");
                interactKey.SetActive(true);
                if (interactPressed && !prescottEvidence.reading)
                {
                    prescottEvidence.PickUp();
                    prescottEvidence.reading = true;
                }
                else if (interactPressed && prescottEvidence.reading)
                {
                    prescottEvidence.CloseWindow();
                }
            }
        }
    }

    public void GEvidenceCollect()
    {
        Ray gEvidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float gRayLength = 4;
        if (Physics.Raycast(gEvidenceRay, out RaycastHit gEvidencehit, gRayLength))
        {
            if (gEvidencehit.collider.gameObject.tag == "GEvidence")
            {
                GangEvidenceCollect gECollect = gEvidencehit.collider.gameObject.GetComponent<GangEvidenceCollect>();
                Debug.Log("Evidence hit!");
                interactKey.SetActive(true);
                if (interactPressed && !gECollect.isgReading)
                {
                    gECollect.GEPickup();
                    gECollect.isgReading = true;
                }
                else if (interactPressed && gECollect.isgReading)
                {
                    gECollect.GECloseWindow();
                }
            }
        }
    }

    public void NorthbyEvidenceCollect()
    {
        Ray gEvidenceRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float gRayLength = 4;
        if (Physics.Raycast(gEvidenceRay, out RaycastHit gEvidencehit, gRayLength))
        {
            if (gEvidencehit.collider.gameObject.tag == "NorthbyEvidence")
            {
                WWNorthbyGangEvidence northbyCollect = gEvidencehit.collider.gameObject.GetComponent<WWNorthbyGangEvidence>();
                Debug.Log("Evidence hit!");
                interactKey.SetActive(true);
                if (interactPressed && !northbyCollect.isgReading)
                {
                    northbyCollect.GEPickup();
                    northbyCollect.isgReading = true;
                }
                else if (interactPressed && northbyCollect.isgReading)
                {
                    northbyCollect.GECloseWindow();
                }
            }
        }
    }
    public void NorthBeachEvidenceCollect()
    {
        Ray NorthBeachRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float NorthBeachLength = 4;
        if (Physics.Raycast(NorthBeachRay, out RaycastHit NorthBeachHit, NorthBeachLength))
        {
            if (NorthBeachHit.collider.gameObject.tag == "NorthBeachEvidence")
            {
                WWNorthBeachEvidence northBeachCollect = NorthBeachHit.collider.gameObject.GetComponent<WWNorthBeachEvidence>();
                Debug.Log("Evidence hit!");
                interactKey.SetActive(true);
                if (interactPressed && !northBeachCollect.isgReading)
                {
                    northBeachCollect.GEPickup();
                    northBeachCollect.isgReading = true;
                }
                else if (interactPressed && northBeachCollect.isgReading)
                {
                    northBeachCollect.GECloseWindow();
                }
            }
        }
    }

    // Responsible for handling all interactions in the game world
    public void HandleInteraction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 4f))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Show interaction prompt, as something is interactable
                interactKey.SetActive(true);

                // Position the interact key above the object being looked at
                Vector3 hitPoint = hitInfo.point;

                Vector3 directionToPlayer = (playerCamera.transform.position - hitPoint).normalized;
                interactKey.transform.position = hitPoint + (directionToPlayer * 0.1f);

                // Make the interact key always face the player
                interactKey.transform.LookAt(playerCamera.transform);

                if (interactable != lastIntercable)
                {
                    lastIntercable?.OnLookAway();
                    lastIntercable = interactable;
                    lastIntercable.OnLookAt();
                }

                if (interactPressed)
                {
                    // Perform interaction
                    interactable.Toggle();
                    interactPressed = false;
                }
            }
            else
            {
                // Negative hit, not a object to interact with
                ResetInteraction();
                Debug.Log("Aww :(");
            }
        }
        else
        {
            // Negative hit, not a object to interact with
            ResetInteraction();
            Debug.Log("Aww :(");
        }
    }

    public void ResetInteraction()
    {
        interactKey.SetActive(false);
        lastIntercable?.OnLookAway();
        lastIntercable = null;
    }

    public void PlaceEvidenceOnBoard()
    {
        Ray placeRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        float placeLength = 8;
        if (Physics.Raycast(placeRay, out RaycastHit placeHit, placeLength))
        {
            if (placeHit.collider.gameObject.tag == "EvidenceBoard" && OTR.GangEvidence && OTR.enabled)
            {
                // EvidencePlace placeEvidence = placeHit.collider.gameObject.GetComponent<EvidencePlace>();
                Debug.Log("Board hit!");
                interactKey.SetActive(true);
                if (interactPressed) // && !placeEvidence.EvidencePlaced)
                {
                    // placeEvidence.StartCoroutine(placeEvidence.EvidenceSwap());
                    // placeEvidence.EvidencePlaced = true;
                    interactKey.SetActive(false);
                }
            }
            if (placeHit.collider.CompareTag("WesteriaEvidenceBoard") && WW.collectedNorthBeachEvidence && WW.enabled)
            {
                WWPlaceEvidence finalEvidence = placeHit.collider.gameObject.GetComponent<WWPlaceEvidence>();
                Debug.Log("Final board hit!");
                interactKey.SetActive(true);
                if (interactPressed && !finalEvidence.EvidencePlaced)
                {
                    finalEvidence.StartCoroutine(finalEvidence.EvidenceSwap());
                    finalEvidence.EvidencePlaced = true;
                    interactKey.SetActive(false);
                }
            }
        }

    }
}
