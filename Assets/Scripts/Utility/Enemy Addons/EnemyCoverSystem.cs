using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCoverSystem : MonoBehaviour
{
    public SphereCollider sCol;
    public float FOV = 90;
    public LayerMask LineofSight;

    public delegate void GainSightEvent(Transform target);
    public GainSightEvent sighted;
    public delegate void LoseSightEvent(Transform target);
    public LoseSightEvent lostSight;

    private Coroutine CheckFOVCoroutine;

    private void Awake()
    {
        sCol = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only track specific targets
        if (other.CompareTag("Player"))
        {
            CheckFOVCoroutine = StartCoroutine(CheckForFieldOV(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lostSight?.Invoke(other.transform);
            if (CheckFOVCoroutine != null) StopCoroutine(CheckFOVCoroutine);
        }
    }

    private bool CheckForFOV(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, directionToTarget);

        // Check if target is within FOV angle
        if (dotProduct >= Mathf.Cos(FOV * Mathf.Deg2Rad))
        {
            if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out RaycastHit hit, sCol.radius, LineofSight))
            {
                if (hit.transform == target)
                {
                    sighted?.Invoke(target);
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator CheckForFieldOV(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (true) // Keep checking while in trigger
        {
            CheckForFOV(target);
            yield return wait;
        }
    }
}