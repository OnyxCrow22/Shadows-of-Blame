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
    private Coroutine MovementCoroutine;
    private EnemyMovementSM esm;

    private void Awake()
    {
        sCol = GetComponent<SphereCollider>();
        esm = GetComponent<EnemyMovementSM>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckForFOV(other.transform))
        {
            CheckFOVCoroutine = StartCoroutine(CheckForFieldOV(esm.enemy.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lostSight?.Invoke(esm.enemy.transform);
        if (CheckFOVCoroutine != null)
        {
            StopCoroutine(CheckFOVCoroutine);
        }
    }

    private bool CheckForFOV(Transform target)
    {
        Vector3 direction = (esm.agent.transform.position - target.transform.position).normalized;
        float dotProduct = Vector3.Dot(esm.enemy.transform.forward, direction);
        if (dotProduct >= Mathf.Cos(FOV))
        {
            if (Physics.Raycast(esm.target.transform.position, direction, out RaycastHit hit, sCol.radius, LineofSight))
            {
                sighted?.Invoke(esm.enemy.transform);
                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckForFieldOV(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (!CheckForFOV(esm.enemy.transform))
        {
            yield return wait;
        }
    }

    public void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }

        MovementCoroutine = StartCoroutine(GetComponent<EnemyMovementSM>().HideIntoCover(esm.enemy.transform));
    }

    public void HandleLostSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
    }
}
