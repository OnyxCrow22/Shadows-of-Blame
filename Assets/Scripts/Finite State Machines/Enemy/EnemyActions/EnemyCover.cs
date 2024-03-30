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

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

    }
}

public class EnemyCoverMaster : MonoBehaviour
{
    private Coroutine MovementCoroutine;
    private Collider[] cols = new Collider[10];
    private EnemyMovementSM esm;

    public void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }

        MovementCoroutine = StartCoroutine(HideIntoCover(target));
    }

    public void HandleLostSight()
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
    }

    private IEnumerator HideIntoCover(Transform target)
    {
        while (true)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = null;
            }

            int hits = Physics.OverlapSphereNonAlloc(esm.enemy.transform.position, esm.eCover.sCol.radius, cols, esm.hideableLayers);

            System.Array.Sort(cols, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                // Samples the position from any collider in the array.
                if (NavMesh.SamplePosition(cols[i].transform.position, out NavMeshHit hit, 2f, esm.agent.areaMask))
                {
                    // Cannot find closestEdge
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, esm.agent.areaMask))
                    {
                        Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit.position}. THIS IS ATTEMPT 1 OF 2");
                    }

                    // Finds the distance between the target and the hit position of the raycast.
                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < esm.HideSensitvity)
                    {
                        // Set the destination to the RayCastHit position.
                        esm.agent.SetDestination(hit.position);
                        Debug.Log($"DIVERTING TO {hit.position}!");
                        break;
                    }
                    else
                    {
                        if (NavMesh.SamplePosition(cols[i].transform.position - (target.position - hit.position).normalized * 2, out NavMeshHit hit2, 2f, esm.agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, esm.agent.areaMask))
                            {
                                Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit2.position} (THIS IS ATTEMPT 2 OF 2");
                            }

                            if (Vector3.Dot(hit2.normal, (target.transform.position - hit2.position).normalized) < esm.HideSensitvity)
                            {
                                esm.agent.SetDestination(hit2.position);
                                Debug.Log($"DIVERTING TO {hit2.position}!");
                                break;
                            }
                        }
                    }
                }
                Debug.Log($"Unable to find any colliders at {cols[i].name}");
                yield return null;
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
}
