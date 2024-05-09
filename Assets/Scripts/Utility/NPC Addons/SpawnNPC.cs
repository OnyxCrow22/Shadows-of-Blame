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

    public GameObject gameManager;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    public IEnumerator Spawn()
    {
        while (AICount < 75)
        {
            int RandomIndex = Random.Range(0, WalkAI.Length);
            int RandomSpawnIndex = Random.Range(0, spawnPoint.Length);
            int RandomDest = Random.Range(0, pedestrianDests.Length);
            int RandomSpawnDelay = Random.Range(0, 4);
            int RandomSpeed = Random.Range(0, 3);
            currentPedDest = pedestrianDests[RandomDest];
            newNPC = Instantiate(WalkAI[RandomIndex], spawnPoint[RandomSpawnIndex].transform.position, Quaternion.identity);

            NPCSM = newNPC.GetComponent<NPCMovementSM>();
            AI = newNPC.GetComponent<NavMeshAgent>();
            newNPC.GetComponent<NPCMovementSM>().spawnedIn = true;

            if (gameManager != null && NPCSM != null)
            {
                NPCSM.police = gameManager.GetComponent<PoliceLevel>();
            }

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

            AI.speed = RandomSpeed;
            yield return new WaitForSeconds(RandomSpawnDelay);
            AICount++;
        }
        if (AICount > 75)
        {
            StopCoroutine(Spawn());
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(newNPC, 1.5f);
    }
}