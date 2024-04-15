using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementSM : EnemyStateMachine
{
    public bool isPatrol = false;
    public bool isChasing = false;
    public bool isMeleeAttack = false;
    public bool isShooting = false;
    public bool isDealDamage = false;
    public bool isHiding = false;
    public bool isAttacking = false;
    public bool attackedPlayer = false;
    public Transform target;
    public Animator eAnim;
    public float damage;
    public float attackDelay = 1.2f;
    public Transform enemy;
    public Transform[] waypoints;
    public Transform ePoint;
    public float distance;
    public GameObject FOV;
    public PlayerMovementSM playsm;
    public NavMeshAgent agent;
    [HideInInspector]
    public int destinations;
    public int RandomIndex;
    [HideInInspector]
    public Collider coverObj;

    public Collider[] cols;

    public LayerMask hideableLayers, Player;
    [Range(-1, 1)]
    public float HideSensitvity;

    // Scripts
    public PlayerHealth health;
    public EnemyHealth eHealth;
    public EnemyCoverSystem eCover;
    public EnemyMeleeSystem eMSystem;
    public AlGun eGun;
    public GangLeaderLogic GGLogic;
    public GangMemberLogic GMLogic;

    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public EnemyPatrol patrolState;
    [HideInInspector]
    public EnemyChase chaseState;
    [HideInInspector]
    public EnemyShoot fireState;
    [HideInInspector]
    public EnemyMelee meleeState;
    [HideInInspector]
    public EnemyCover coverState;

    private void Awake()
    {
        idleState = new EnemyIdle(this);
        patrolState = new EnemyPatrol(this);
        chaseState = new EnemyChase(this);
        fireState = new EnemyShoot(this);
        meleeState = new EnemyMelee(this);
        coverState = new EnemyCover(this);

        eCover.sighted += eCover.HandleGainSight;
        eCover.lostSight += eCover.HandleLostSight;
    }

    protected override EnemyBaseState GetInitialState()
    {
        return idleState;
    }

    public void GoToNextPoint()
    {
        // End of path
        if (waypoints.Length == 0)
        {
            return;
        }
        agent.destination = waypoints[destinations].position;
        destinations = (destinations + 1) % waypoints.Length;
    }

    public void RandomIndexCheck()
    {
        RandomIndex = Random.Range(0, cols.Length);
        coverObj = cols[RandomIndex];
    }

    public IEnumerator HideIntoCover(Transform target)
    {
        while (true)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = null;
            }

            int hits = Physics.OverlapSphereNonAlloc(agent.transform.position, eCover.sCol.radius, cols, hideableLayers);

            System.Array.Sort(cols, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                // Samples the position from any collider in the array.
                if (NavMesh.SamplePosition(cols[i].transform.position, out NavMeshHit hit, 2f, agent.areaMask))
                {
                    // Cannot find closestEdge
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, agent.areaMask))
                    {
                        Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit.position}. THIS IS ATTEMPT 1 OF 2");
                    }

                    // Finds the distance between the target and the hit position of the raycast.
                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < HideSensitvity)
                    {
                        // Set the destination to the RayCastHit position.
                        agent.SetDestination(hit.position);
                        agent.isStopped = true;
                        Debug.Log($"DIVERTING TO {hit.position}!");
                        break;
                    }
                    else
                    {
                        if (NavMesh.SamplePosition(cols[i].transform.position, out NavMeshHit hit2, 2f, agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, agent.areaMask))
                            {
                                Debug.LogError($"UNABLE TO FIND EDGE CLOSE TO {hit2.position} (THIS IS ATTEMPT 2 OF 2");
                            }

                            if (Vector3.Dot(hit2.normal, (target.transform.position - hit2.position).normalized) < HideSensitvity)
                            {
                                agent.SetDestination(hit2.position);
                                agent.isStopped = true;
                                Debug.Log($"DIVERTING TO {hit2.position}!");
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
            return Vector3.Distance(agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(agent.transform.position, B.transform.position));
        }
    }
}
