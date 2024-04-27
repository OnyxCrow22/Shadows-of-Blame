using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNPC : MonoBehaviour
{
    public GameObject[] WalkAI;
    public int AICount;
    public GameObject player;
    public LayerMask pavement;
    public GameObject[] spawnPoint;
    [HideInInspector]
    public GameObject newNPC;
    [HideInInspector]
    public NavMeshAgent AI;
    [HideInInspector]
    public NPCMovementSM NPCSM;
    public GameObject[] pedestrianDests;
    [HideInInspector]
    public GameObject currentPedDest;

    public IEnumerator Spawn()
    {
        int AICount = 0;
        while (AICount < 75)
        {
            int RandomIndex = Random.Range(0, WalkAI.Length);
            int RandomSpawnIndex = Random.Range(0, spawnPoint.Length);
            int RandomDest = Random.Range(0, pedestrianDests.Length);
            currentPedDest = pedestrianDests[RandomDest];
            newNPC = Instantiate(WalkAI[RandomIndex], spawnPoint[RandomSpawnIndex].transform.position, Quaternion.identity);

            NPCSM = newNPC.GetComponent<NPCMovementSM>();
            AI = newNPC.GetComponent<NavMeshAgent>();
            newNPC.GetComponent<NPCMovementSM>().spawnedIn = true;
            ;
            if (player != null)
            {
                PlayerMovementSM playsm = player.GetComponent<PlayerMovementSM>();
                player.GetComponent<GameObject>();
                if (playsm != null && NPCSM != null)
                {
                    NPCSM.playsm = playsm;
                    NPCSM.player = player;
                }

            }

            if (NPCSM != null)
            {
                AI.SetDestination(currentPedDest.transform.position);
            }
            yield return new WaitForSeconds(0.05f);
            AICount++;
        }
        if (AICount > 75)
        {
            StopCoroutine(Spawn());
        }
    }
}