using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceLevel : MonoBehaviour
{
    public GameObject[] attainLevels;
    public GameObject policeOfficer;
    public GameObject[] policeVehicles;
    public GameObject border;
    public PlayerMovementSM playsm;
    PoliceMovementSM police;
    public bool attainingLevel;
    public static int levelStage;
    public static bool giveLevel;
    public int bustedTimer = 5;
    GameObject[] pedestrianSpawns;
    GameObject[] vehicleSpawns;
    GameObject[] policeDests;
    GameObject currentPoliceDest;
    GameObject newPoliceCar;
    GameObject newPolicePedestrian;
    NavMeshAgent PoliceAI;
    Vector3 lastKnownPos;

    private void Start()
    {
        StartCoroutine(PolicePedestrians());
        police = newPolicePedestrian.GetComponent<PoliceMovementSM>();
    }

    private void Update()
    {
        AddNewLevel();
        LoseLevel();
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

    public void LoseLevel()
    {
        if (attainingLevel == true && giveLevel == true)
        {
            giveLevel = false;
            attainingLevel = false;
            StartCoroutine(RemoveLevel());
            if (levelStage == 0)
            {
                border.SetActive(false);
            }
        }
    }

    public void LostVisual()
    {
        Ray visualRay = new Ray(police.PoliceFOV.transform.position, police.playsm.player.transform.position);
        RaycastHit visualHit;
        float visualRange = 40;
        if (Physics.Raycast(visualRay, out visualHit, visualRange))
        {
            if (visualHit.collider.name != "Player" || policeOfficer != GameObject.FindGameObjectWithTag("Police"))
            {
                LoseLevel();
                StartCoroutine(SearchForPlayer());
                StopCoroutine(AddLevel());
                attainingLevel = false;
                Debug.Log("ARGH! WE LOST THE PLAYER! Conduct a search!");
            }

            else if (policeOfficer == null)
            {
                LoseLevel();
                StartCoroutine(RemoveLevel());
            }
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

    public IEnumerator PolicePedestrians()
    {
        int policePedestriansCount = 0;
        while (policePedestriansCount < 15)
        {
            pedestrianSpawns = GameObject.FindGameObjectsWithTag("Spawn");
            int SpawnIndex = Random.Range(0, pedestrianSpawns.Length);
            policeDests = GameObject.FindGameObjectsWithTag("PedDests");
            int RandomPoliceDest = Random.Range(0, policeDests.Length);
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

            if (levelStage >= 1)
            {
                PoliceAI.SetDestination(player.transform.position);
            }

            else if (levelStage == 0)
            {
                PoliceAI.SetDestination(currentPoliceDest.transform.position);
            }
            yield return new WaitForSeconds(0.05f);
            policePedestriansCount++;
        }

        if (policePedestriansCount > 15)
        {
            StopCoroutine(PolicePedestrians());
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
            vehicleSpawns = GameObject.FindGameObjectsWithTag("PoliceSpawns");
            int RandomIndex = Random.Range(0, policeVehicles.Length);
            int RandomSpawns = Random.Range(0, vehicleSpawns.Length);

            newPoliceCar = Instantiate(policeVehicles[RandomIndex], vehicleSpawns[RandomSpawns].transform.position, Quaternion.identity);
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
        int runCount = 0;
        while (runCount < 5)
        {
            attainLevels[levelStage - 1].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attainLevels[levelStage - 1].SetActive(false);
            yield return new WaitForSeconds(0.5f);
            attainLevels[levelStage - 1].SetActive(true);
            runCount++;
        }

        if (runCount > 5)
        {
            StopCoroutine(AddLevel());
            attainLevels[levelStage].SetActive(true);
        }
    }

    public IEnumerator RemoveLevel()
    {
        int runCount = 0;
        while (runCount < 2)
        {
            attainLevels[levelStage - 1].SetActive(false);
            yield return new WaitForSeconds(0.5f);
            runCount++;
        }

        if (runCount >= 2)
        {
            StopCoroutine(RemoveLevel());
            attainLevels[levelStage].SetActive(false);
            attainingLevel = false;
            levelStage--;
        }
    }

    public IEnumerator SearchForPlayer()
    {
        PoliceAI.SetDestination(lastKnownPos);
        yield return new WaitForSeconds(10);
        attainingLevel = false;
        GameObject[] policeO = GameObject.FindGameObjectsWithTag("Police");

        if (policeO.Length == 0)
        {
            StartCoroutine(RemoveLevel());
        }
    }
}
