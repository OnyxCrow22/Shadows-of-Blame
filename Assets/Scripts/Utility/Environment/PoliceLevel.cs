using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceLevel : MonoBehaviour
{
    public GameObject[] attainLevels;
    public GameObject[] policeOfficers;
    public GameObject[] policeVehicles;
    public GameObject border;
    public PlayerMovementSM playsm;
    public bool attainingLevel;
    public static int levelStage;
    public static bool giveLevel;
    public int bustedTimer = 5;
    GameObject[] spawns;
    GameObject[] pedestrianSpawns;
    GameObject newPoliceCar;
    GameObject newPolicePedestrian;
    NavMeshAgent PoliceAI;

    private void Update()
    {
        AddNewLevel();
        SpawnPolice();
        SpawnPedestrianPolice();
        SpawnPoliceCars();
    }

    public void AddNewLevel()
    {
        if (attainingLevel == false && giveLevel == true)
        {
            giveLevel = false;
            border.SetActive(true);
            attainingLevel = true;
            StartCoroutine(AddLevel());
        }
    }

    public void SpawnPolice()
    {
        if (playsm.inVehicle == false)
        {
            SpawnPedestrianPolice();
        }
        else if (playsm.inVehicle == true)
        {
            SpawnPoliceCars();
        }
    }

    public void SpawnPedestrianPolice()
    {
        StartCoroutine(PolicePedestrians());
    }

    public void ShootPlayer()
    {
        AlGun AIgun = newPolicePedestrian.AddComponent<AlGun>();
        AIgun.ShootGun();
    }

    IEnumerator PolicePedestrians()
    {
        int policePedestriansCount = 0;
        while (policePedestriansCount < 15 && !playsm.inVehicle)
        {
            pedestrianSpawns = GameObject.FindGameObjectsWithTag("Spawn");
            int RandomIndex = Random.Range(0, policeOfficers.Length);
            int SpawnIndex = Random.Range(0, spawns.Length);
            newPolicePedestrian = Instantiate(policeOfficers[RandomIndex], pedestrianSpawns[SpawnIndex].transform.position, Quaternion.identity);

            PoliceAI = newPolicePedestrian.GetComponent<NavMeshAgent>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                PoliceAI.SetDestination(player.transform.position);
            }
            yield return new WaitForSeconds(0.25f);
            policePedestriansCount++;
        }

        if (policePedestriansCount > 15 || playsm.inVehicle)
        {
            StopCoroutine(PolicePedestrians());
            StartCoroutine(PoliceCars());
        }

        if (playsm.weapon.gunEquipped || levelStage >= 2)
        {
            ShootPlayer();
        }
    }

    public void SpawnPoliceCars()
    {
        StartCoroutine(PoliceCars());
    }

    IEnumerator PoliceCars()
    {
        int policeCount = 0;
        while (policeCount < 5 && playsm.inVehicle)
        {
            spawns = GameObject.FindGameObjectsWithTag("PoliceSpawns");
            int RandomIndex = Random.Range(0, policeVehicles.Length);
            int RandomSpawns = Random.Range(0, spawns.Length);

            newPoliceCar = Instantiate(policeVehicles[RandomIndex], spawns[RandomSpawns].transform.position, Quaternion.identity);
            PoliceAI = newPoliceCar.GetComponent<NavMeshAgent>();


            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<PlayerMovementSM>();
                PoliceAI.SetDestination(player.transform.position);
            }
            yield return new WaitForSeconds(0.25f);

            PoliceAI.SetDestination(playsm.player.transform.position);
            policeCount++;
        }

        if (policeCount > 5 || !playsm.inVehicle)
        {
            StopCoroutine(PoliceCars());
        }

        if (playsm.weapon.gunEquipped || !playsm.inVehicle)
        {
            StopCoroutine(PoliceCars());
            StartCoroutine(PolicePedestrians());
        }
    }

    public IEnumerator AddLevel()
    {
        while (true)
        {
            attainLevels[levelStage - 1].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attainLevels[levelStage - 1].SetActive(false);
        }
    }
}
