using UnityEngine;
using System;

public class VisionSensor : MonoBehaviour
{
    public float viewDistance = 30f;
    public float viewAngle = 60f;
    public float detectionTime = 1.5f;

    public Transform eye;
    public LayerMask visionMask;

    public static event Action<GameObject> OnPlayerDetected;
    public static event Action<GameObject> OnPlayerLost;

    private float suspicion;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        CheckVision();
    }

    private void CheckVision()
    {
        Vector3 dir = (player.position - eye.position).normalized;

        // Check angle
        if (Vector3.Angle(eye.forward, dir) > viewAngle)
        {
            LoseSuspicion();
            return;
        }

        // Check distance
        float dist = Vector3.Distance(eye.position, player.position);
        if (dist > viewDistance)
        {
            LoseSuspicion();
            return;
        }

        // Check line of sight
        if (Physics.Raycast(eye.position, dir, out RaycastHit hit, viewDistance, visionMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                BuildSuspicion();
                return;
            }
        }

        LoseSuspicion();
    }

    private void BuildSuspicion()
    {
        suspicion += Time.deltaTime;

        if (suspicion >= detectionTime)
            OnPlayerDetected?.Invoke(gameObject);
    }

    private void LoseSuspicion()
    {
        if (suspicion > 0)
        {
            suspicion -= Time.deltaTime;

            if (suspicion <= 0)
                OnPlayerLost?.Invoke(gameObject);
        }
    }
}
