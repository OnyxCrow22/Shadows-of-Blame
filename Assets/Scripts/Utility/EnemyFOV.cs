using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public AlertStages stages;
    [Range(0, 100)] public float alertLevel;
    public float timeSpentinFOV, fovTimer;
    public bool canSeePlayer = false;

    public enum AlertStages
    {
        Patrolling,
        NoticedSomething,
        Alerted
    }

    private void Update()
    {
        alertLevel = Mathf.Clamp(alertLevel, 0, 100);
        timeSpentinFOV = Mathf.Clamp(timeSpentinFOV, 0, 5);
        fovTimer = Mathf.Clamp(fovTimer, 0, 5);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alertLevel += timeSpentinFOV;
            stages = AlertStages.NoticedSomething;
            timeSpentinFOV += fovTimer;
            fovTimer -= Time.deltaTime;
            canSeePlayer = true;

            if (timeSpentinFOV >= 5 || fovTimer <= 0 || alertLevel == 100)
            {
                alertLevel = 100;
                stages = AlertStages.Alerted;
                timeSpentinFOV = 5;
                fovTimer = 0;
                canSeePlayer = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alertLevel -= timeSpentinFOV;
            fovTimer += Time.deltaTime;
            canSeePlayer = false;

            if (timeSpentinFOV <= 0 || fovTimer >= 5 || alertLevel == 0)
            {
                alertLevel = 0;
                timeSpentinFOV = 0;
                fovTimer = 5;
                stages = AlertStages.Patrolling;
                canSeePlayer = false;
            }
        }
    }
}
