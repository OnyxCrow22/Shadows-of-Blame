using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastMaster : MonoBehaviour
{
    public GameObject interactKey;
    public float forwardRayLength = 3f;

    private IInteractable lastInteracted = null;
    private bool interactPressed = false;

    void Update()
    {
        PerformInteractionRaycast();
        interactPressed = false; // Reset input each frame
    }

    public void OnInteract(InputAction.CallbackContext ctx) => interactPressed = ctx.performed;

    private void PerformInteractionRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, forwardRayLength))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                ShowPrompt(hit);

                if (interactable != lastInteracted)
                {
                    lastInteracted?.OnLookAway();
                    lastInteracted = interactable;
                    lastInteracted.OnLookAt();
                }

                if (interactPressed)
                {
                    // Pass the player or relevant object to the interactable
                    interactable.OnInteract(this.gameObject);
                }
                return;
            }
        }

        ClearInteraction();
    }

    private void ShowPrompt(RaycastHit hit)
    {
        interactKey.SetActive(true);
        interactKey.transform.position = hit.point + (Vector3.up * 0.2f);
    }

    private void ClearInteraction()
    {
        if (lastInteracted != null)
        {
            lastInteracted.OnLookAway();
            lastInteracted = null;
        }
        interactKey.SetActive(false);
    }
}