using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCoverMaster : MonoBehaviour
{
    public LayerMask hidableLayers;
    public EnemyCoverSystem eCover;
    public NavMeshAgent enemy;
    [Range(-1, 1f)] public float hideSensitivity;
    private Coroutine MovementCoroutine;
    private Collider[] cols = new Collider[10];

    private void Awake()
    {
        eCover.sighted += HandleGainSight;
        eCover.lostSight += HandleLostSight;
    }

    public void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }

        MovementCoroutine = StartCoroutine(HideIntoCover(target));
    }

    public void HandleLostSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
    }

    public IEnumerator HideIntoCover(Transform target)
    {
        bool CoverFound = false;

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = null;
        }

        int hits = Physics.OverlapSphereNonAlloc(enemy.transform.position, eCover.sCol.radius, cols, hidableLayers);

        System.Array.Sort(cols, ColliderArraySortComparer);

        for (int i = 0; i < hits; i++)
        {
            // Samples the position from any collider in the array.
            if (NavMesh.SamplePosition(cols[i].transform.position, out NavMeshHit hit, 2f, enemy.areaMask))
            {
                // Cannot find closestEdge
                if (!NavMesh.FindClosestEdge(hit.position, out hit, enemy.areaMask))
                {
                    Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit.position}. THIS IS ATTEMPT 1 OF 2");
                }

                // Finds the distance between the target and the hit position of the raycast.
                if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < hideSensitivity)
                {
                    // Set the destination to the RayCastHit position.
                    enemy.SetDestination(hit.position);
                    Debug.Log($"DIVERTING TO {hit.position}!");
                    CoverFound = true;
                    break;
                }
                else
                {
                    if (NavMesh.SamplePosition(cols[i].transform.position, out NavMeshHit hit2, 2f, enemy.areaMask))
                    {
                        if (!NavMesh.FindClosestEdge(hit2.position, out hit2, enemy.areaMask))
                        {
                            Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit2.position} (THIS IS ATTEMPT 2 OF 2");
                        }

                        if (Vector3.Dot(hit2.normal, (target.transform.position - hit2.position).normalized) < hideSensitivity)
                        {
                            enemy.SetDestination(hit2.position);
                            Debug.Log($"DIVERTING TO {hit2.position}!");
                            CoverFound = true;
                            yield break;
                        }
                    }
                }
                yield return null;
            }

            if (CoverFound)
            {
                break;
            }
        }
    }
    public int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            // Compares the distance between Collider A and B.
            return Vector3.Distance(enemy.transform.position, A.transform.position).CompareTo(Vector3.Distance(enemy.transform.position, B.transform.position));
        }
    }
}
