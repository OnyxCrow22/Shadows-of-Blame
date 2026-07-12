using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNPC : MonoBehaviour
{
    [Header("NPC Setup")]
    public GameObject[] npcPrefabs;
    public int maxNPCs = 75;

    [Header("References")]
    public GameObject player;
    public PoliceLevel policeLevel;

    private GameObject[] spawnPoints;
    private int npcCount;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (npcCount < maxNPCs)
        {
            SpawnOneNPC();
            npcCount++;

            yield return new WaitForSeconds(Random.Range(0f, 4f));
        }
    }

    private void SpawnOneNPC()
    {
        int prefabIndex = Random.Range(0, npcPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject npc = Instantiate(
            npcPrefabs[prefabIndex],
            spawnPoints[spawnIndex].transform.position,
            Quaternion.identity
        );

        NPCMovementSM sm = npc.GetComponent<NPCMovementSM>();
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();

        // Assign core references only
        sm.player = player;
        sm.playsm = player.GetComponent<PlayerMovementSM>();
        sm.police = policeLevel;

        // Mark as spawned
        sm.spawnedIn = true;

        // Optional random speed
        agent.speed = Random.Range(1f, 3f);
    }
}
