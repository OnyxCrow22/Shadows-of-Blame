using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class SpawnNPC : MonoBehaviour
{
    [Header("NPC Setup")]
    public GameObject[] npcPrefabs;
    public int maxNPCs = 75;

    [Header("References")]
    public GameObject player;
    public PoliceLevel policeLevel;
    private PlayerMovementSM cachedPSM;
    private NPCMovementSM sm;

    public IObjectPool<GameObject>[] NPCPool;

    private GameObject[] spawnPoints;
    private int npcCount;

    private void Start()
    {
        if (player != null)
        {
            sm.playsm = cachedPSM;
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

        InitializePool();
        StartCoroutine(SpawnLoop());
    }

    private void InitializePool()
    {
        NPCPool = new IObjectPool<GameObject>[npcPrefabs.Length];

        for (int i = 0;  i < npcPrefabs.Length; i++)
        {
            int prefabIndex = i;
            NPCPool[i] = new ObjectPool<GameObject>(createFunc: () => Instantiate(npcPrefabs[prefabIndex]),
                actionOnGet: (npc) => npc.SetActive(true),
                actionOnRelease: (npc) => npc.SetActive(false),
                actionOnDestroy: (npc) => Destroy(npc),
                collectionCheck: false,
                defaultCapacity: Mathf.Max(1, maxNPCs / npcPrefabs.Length),
                maxSize: maxNPCs);
        }

    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (npcCount < maxNPCs && spawnPoints.Length > 0)
            {
                SpawnOneNPC();
            }

            yield return new WaitForSeconds(Random.Range(0f, 4f));
        }
    }

    private void SpawnOneNPC()
    {
        int prefabIndex = Random.Range(0, npcPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject npc = NPCPool[prefabIndex].Get();

        npc.transform.SetPositionAndRotation(
            spawnPoints[spawnIndex].transform.position,
            Quaternion.identity
        );

        sm = npc.GetComponent<NPCMovementSM>();
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();

        // Assign core references only
        sm.player = player;
        sm.playsm = player.GetComponent<PlayerMovementSM>();
        sm.police = policeLevel;

        // Mark as spawned
        sm.spawnedIn = true;

        // Store the index
        sm.prefabPoolIndex = prefabIndex;

        // Optional random speed
        agent.speed = Random.Range(1f, 3f);
        npcCount++;
    }

    public void DespawnNPC(GameObject NPC,  int prefabIndex)
    {
        NPCPool[prefabIndex].Release(NPC);
        npcCount--;
    }
}
