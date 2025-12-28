using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
        EvidenceCollecting();
        DoorHandling();
        CarDoors();
        GEvidenceCollect();
        HParkEvidenceCollect();
        PrescottEvidenceCollect();
        NorthbyEvidenceCollect();
        NorthBeachEvidenceCollect();
        PlaceEvidenceOnBoard();
        LiftOperate();

        interactPressed = false;
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            interactPressed = true;
    }

    public void DoorHandling()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        RaycastHit hit;
        float rayLength = 4;
        if (!Physics.Raycast(ray, out hit, rayLength))
        {
            lastIntercable?.OnLookAway();
            lastIntercable = null;
            return;
        }
        if (!hit.collider.gameObject.CompareTag("Door"))
        {
            lastIntercable?.OnLookAway();
            lastIntercable = null;
            return;
        }

        IIteractable interactable = hit.collider.gameObject.GetComponent<IIteractable>();
        if (IIteractable == null)
        {
            lastIntercable?.OnLookAway();
            lastIntercable = null;
            return;
        }

        if (IIteractable != lastIntercable)
        {
            lastIntercable?.OnLookAway();
            lastIntercable = IIteractable;
        }
        IIteractable.OnLookAt();

        if (interactPressed)
        {
            IIteractable.Toggle();
            interactKey.SetActive(false);
        }
    }

    public void CarDoors()
    {
        Ray carDoorRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        RaycastHit carDoorHit;
        float rayLength = 2;
        if (Physics.Raycast(carDoorRay, out carDoorHit, rayLength))
        {
            if (carDoorHit.collider.gameObject.tag == "VehicleDoor")
            {
                VehicleEnterExit veh = carDoorHit.collider.gameObject.GetComponent<VehicleEnterExit>();
                interactKey.SetActive(true);
                veh.canEnter = true;
                if (interactPressed && !veh.inVehicle)
                {
                    StartCoroutine(veh.EnteringVehicle());
                    veh.canEnter = false;
                    interactKey.SetActive(false);
                }
                else if (interactPressed && veh.inVehicle)
                {
                    StartCoroutine(veh.ExitingVehicle());
                    veh.canExit = true;
                    veh.canEnter = false;
                    interactKey.SetActive(false);
                }
            }
        }
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
                if (interactPressed && !collectEvidence.reading)
                {
                    collectEvidence.PickUp();
                    collectEvidence.reading = true;
                }
                else if (interactPressed && collectEvidence.reading)
                {
                    collectEvidence.CloseWindow();
                }
            }
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

    public void PlaceEvidenceOnBoard()
    {
        Ray placeRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        float placeLength = 8;
        if (Physics.Raycast(placeRay, out RaycastHit placeHit, placeLength))
        {
            if (placeHit.collider.gameObject.tag == "EvidenceBoard" && OTR.GangEvidence && OTR.enabled)
            {
                EvidencePlace placeEvidence = placeHit.collider.gameObject.GetComponent<EvidencePlace>();
                Debug.Log("Board hit!");
                interactKey.SetActive(true);
                if (interactPressed && !placeEvidence.EvidencePlaced)
                {
                    placeEvidence.StartCoroutine(placeEvidence.EvidenceSwap());
                    placeEvidence.EvidencePlaced = true;
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

    public void LiftOperate()
    {
        Ray liftRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.black);
        float liftLength = 8;
        if (Physics.Raycast(liftRay, out RaycastHit liftHit, liftLength))
        {
            if (liftHit.collider.tag == "LiftObj")
            {
                Lift newLift = liftHit.collider.gameObject.GetComponent<Lift>();
                Debug.Log("Going up or down?");
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (newLift.atBottom)
                    {
                        StartCoroutine(newLift.OperateLift());
                        interactKey.SetActive(false);
                        buttonPressed = true;
                        inLift = true;
                        Debug.Log("Please stand clear of the doors. We are now going up to the 21st Floor.");
                    }
                    else if (newLift.atTop)
                    {
                        StartCoroutine(newLift.GoingDown());
                        interactKey.SetActive(false);
                        buttonPressed = true;
                        inLift = true;
                        Debug.Log("Please stand clear of the doors. We are now going up to the ground floor.");
                    }
                }
            }

            if (liftHit.collider.tag == "Button")
            {
                LiftCall callLift = liftHit.collider.gameObject.GetComponent<LiftCall>();
                interactKey.SetActive(true);
                if (interactPressed)
                {
                    if (callLift.liftToCall.atBottom)
                    {
                        callLift.Up();
                        inLift = false;
                        interactKey.SetActive(false);
                    }
                    else if (callLift.liftToCall.atTop)
                    {
                        callLift.Down();
                        inLift = false;
                        interactKey.SetActive(false);
                    }
                }
            }
        }
    }
}
