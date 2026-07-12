using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCover : EnemyBaseState
{
    EnemyMovementSM esm;
    private Coroutine coverCoroutine;

    public EnemyCover(EnemyMovementSM enemyStateMachine) : base("Cover", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        // Start the persistent cover-finding loop
        coverCoroutine = esm.StartCoroutine(HideIntoCover(esm.target));
    }

    public override void Exit()
    {
        base.Exit();
        // Crucial: stop the loop when leaving this state
        if (coverCoroutine != null) esm.StopCoroutine(coverCoroutine);
        esm.agent.isStopped = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Does the agent have a path pending?
        if (esm.agent.pathPending) return;

        // Is the agent close to the destination?
        if (esm.agent.remainingDistance <= esm.agent.stoppingDistance)
        {
            Debug.Log("REACHED DESTINATION!");
            esm.eAnim.SetBool("patrolling", false);
            esm.agent.isStopped = true;
            AudioManager.manager.Stop("walk");

            // Switch state
            enemyStateMachine.ChangeState(esm.idleState);
        }
    }

    public IEnumerator HideIntoCover(Transform target)
    {
        while (true)
        {
            // Access fields through the esm reference
            for (int i = 0; i < esm.cols.Length; i++) { esm.cols[i] = null; }

            int hits = Physics.OverlapSphereNonAlloc(esm.agent.transform.position, esm.eCover.sCol.radius, esm.cols, esm.hideableLayers);

            System.Array.Sort(esm.cols, esm.ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {
                if (esm.cols[i] == null) continue;

                if (NavMesh.SamplePosition(esm.cols[i].transform.position, out NavMeshHit hit, 2f, esm.agent.areaMask))
                {
                    if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < esm.HideSensitvity)
                    {
                        esm.agent.SetDestination(hit.position);
                        esm.agent.isStopped = false; // Allow movement to cover
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}