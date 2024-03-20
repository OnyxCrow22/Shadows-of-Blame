using System.Collections;
using UnityEngine;

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
        if (!CheckForFOV(other.transform))
        {
            CheckFOVCoroutine = StartCoroutine(CheckForFieldOV(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lostSight?.Invoke(other.transform);
        if (CheckFOVCoroutine != null)
        {
            StopCoroutine(CheckFOVCoroutine);
        }
    }

    private bool CheckForFOV(Transform target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if (dotProduct >= Mathf.Cos(FOV))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, sCol.radius, LineofSight))
            {
                sighted?.Invoke(target);
                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckForFieldOV(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (!CheckForFOV(target))
        {
            yield return wait;
        }
    }
}
