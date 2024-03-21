using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCover : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyCover(EnemyMovementSM enemyStateMachine) : base("Cover", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public void Awake()
    {
        esm.eCover.sighted += esm.eCover.HandleGainSight;
        esm.eCover.lostSight += esm.eCover.HandleLostSight;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public IEnumerator HideIntoCover(Transform target)
    {
        while (true)
        {
            for (int i = 0; i < esm.cols.Length; i++)
            {
                esm.cols[i] = null;
            }

            int hits = Physics.OverlapSphereNonAlloc(esm.agent.transform.position, esm.eCover.sCol.radius, esm.cols, esm.hidableLayers);

            System.Array.Sort(esm.cols, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                // Samples the position from any collider in the array.
                if (NavMesh.SamplePosition(esm.cols[i].transform.position, out NavMeshHit hit, 2f, esm.agent.areaMask))
                {
                    // Cannot find closestEdge
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, esm.agent.areaMask))
                    {
                        Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit.position}. THIS IS ATTEMPT 1 OF 2");
                    }

                    // Finds the distance between the target and the hit position of the raycast.
                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < esm.hideSensitivty)
                    {
                        // Set the destination to the RayCastHit position.
                        esm.agent.SetDestination(hit.position);
                        break;
                    }
                    else
                    {
                        if (NavMesh.SamplePosition(esm.cols[i].transform.position, out NavMeshHit hit2, 2f, esm.agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, esm.agent.areaMask))
                            {
                                Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit2.position} (THIS IS ATTEMPT 2 OF 2");
                            }

                            if (Vector3.Dot(hit2.normal, (target.position - hit2.position).normalized) < esm.hideSensitivty)
                            {
                                esm.agent.SetDestination(hit2.position);
                                break;
                            }
                        }
                    }
                    yield return null;
                }
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
            return Vector3.Distance(esm.agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(esm.agent.transform.position, B.transform.position));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

}
