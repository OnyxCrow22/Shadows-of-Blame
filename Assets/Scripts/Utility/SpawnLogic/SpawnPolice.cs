using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPolice : MonoBehaviour
{
    public GameObject policeOfficer;
    public GameObject[] policeVehicles;
    public PlayerMovementSM playsm;
    PoliceMovementSM police;
    public GameObject[] pedestrianSpawns;
    public GameObject[] policeDests;
    GameObject currentPoliceDest;
    GameObject newPolicePedestrian;
    NavMeshAgent PoliceAI;
    Vector3 lastKnownPos;

    private void Start()
    {
        StartCoroutine(PolicePedestrians());
        police = newPolicePedestrian.GetComponent<PoliceMovementSM>();
    }
    public IEnumerator PolicePedestrians()
    {
        int policePedestriansCount = 0;
        while (policePedestriansCount < 15)
        {
            int SpawnIndex = Random.Range(0, pedestrianSpawns.Length);
            int RandomPoliceDest = Random.Range(0, policeDests.Length);
            int RandomSpawnDelay = Random.Range(0, 4);
            int RandomSpeed = Random.Range(0, 3);
            currentPoliceDest = policeDests[RandomPoliceDest];
            newPolicePedestrian = Instantiate(policeOfficer, pedestrianSpawns[SpawnIndex].transform.position, Quaternion.identity);

            PoliceMovementSM policesm = newPolicePedestrian.GetComponent<PoliceMovementSM>();
            PoliceAI = newPolicePedestrian.GetComponent<NavMeshAgent>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                PlayerMovementSM playsm = player.GetComponent<PlayerMovementSM>();
                player.GetComponent<GameObject>();
                if (playsm != null && (policesm != null))
                {
                    policesm.player = player;
                    policesm.playsm = playsm;
                }
            }

            if (PoliceLevel.policeLevels >= 1)
            {
                PoliceAI.SetDestination(player.transform.position);
            }

            else if (PoliceLevel.policeLevels == 0)
            {
                PoliceAI.SetDestination(currentPoliceDest.transform.position);
            }
            PoliceAI.speed = RandomSpeed;
            yield return new WaitForSeconds(RandomSpawnDelay);
            policePedestriansCount++;
        }

        if (policePedestriansCount > 15)
        {
            StopCoroutine(PolicePedestrians());
        }

        if (playsm.inVehicle)
        {
            
        }
    }
}
