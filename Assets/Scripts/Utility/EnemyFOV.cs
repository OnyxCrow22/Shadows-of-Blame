using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public AlertStages stages;
    [Range(0, 100)] public float alertLevel;
    public float timeSpentinFOV, fovTimer;
    public float alertLevelSpeed;

    public enum AlertStages
    {
        Patrolling,
        NoticedSomething,
        Alerted
    }

    private void Update()
    {
        alertLevel = Mathf.Clamp(alertLevel, 0, 100);
        alertLevelSpeed = Mathf.Clamp(alertLevelSpeed, 0, 100);
        timeSpentinFOV = Mathf.Clamp(timeSpentinFOV, 0, 5);
        fovTimer = Mathf.Clamp(fovTimer, 0, 5);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alertLevel += alertLevelSpeed += timeSpentinFOV;
            stages = AlertStages.NoticedSomething;
            timeSpentinFOV += fovTimer;

            if (timeSpentinFOV >= 5 || alertLevel == 100)
            {
                alertLevel = 100;
                stages = AlertStages.Alerted;
                timeSpentinFOV = 5;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alertLevel -= alertLevelSpeed -= timeSpentinFOV;
            timeSpentinFOV -= fovTimer;

            if (timeSpentinFOV <= 0 || alertLevel == 0)
            {
                alertLevel = 0;
                timeSpentinFOV = 0;
                alertLevelSpeed = 0;
                stages = AlertStages.Patrolling;
            }
        }
    }
}
