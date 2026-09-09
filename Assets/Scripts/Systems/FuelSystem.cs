using System;
using UnityEngine;
using UnityEngine.Events;

public enum FuelType { Petrol, Diesel, Electricity};

public class FuelSystem : MonoBehaviour
{
    [Header("Engine Configuration")]
    public FuelType fuelType = FuelType.Petrol; // Set to Petrol by default; Can be changed.
    [Range(0f, 100f)] public float currentFuel = 100f; // Set to 100% by default.
    public bool EngineOn;

    [Header("Vehicle Dynamics")]
    public float currentVehicleSpeed = 0f; // Set to 0MPH by default.

    [Header("Chasing and Refuel Sub-system")]
    public bool inPoliceChase;
    public int currentChaseRefuelAllowance = 0; // Set to 0 by default.
    private const int maximumChaseRefuelAllownace = 4; // Capped refuels to four during chases.
    protected bool fuelStationsLockedDown;

    [Header("Fuel Events")]
    public UnityEvent<FuelType> OnLowFuel; // Activates at 25%
    public UnityEvent OnFuelDepleted; // Activates at 0%

    private bool TriggerLowFuelRoute;
    private bool Refuelling;
    private float refuelTimer = 0f;
    private const float RefuelDuration = 10f;

    private void Update()
    {
        if (Refuelling)
        {
            ProcessRefuelling();
            return;
        }

        if (!EngineOn || currentFuel <= 0f) return;

        ConsumeFuel();
        CheckFuelThresholds();
    }

    public void ConsumeFuel()
    {
        float consumeFuelPerTenMin;

        switch (currentVehicleSpeed)
        {
            case <= 0.5f:
                consumeFuelPerTenMin = 2.0f; // 2%
                break;
            case > 70:
                consumeFuelPerTenMin = 8.0f; // 8%
                break;
            default:
                consumeFuelPerTenMin = 4.0f; // 4%
                break;
        }

        float percentPerSecond = consumeFuelPerTenMin / 600.0f;
        currentFuel -= percentPerSecond * Time.deltaTime;
        currentFuel = Mathf.Max(currentFuel, 0f);
    }

    public void CheckFuelThresholds()
    {
        if (currentFuel <= 25.0f && !TriggerLowFuelRoute)
        {
            TriggerLowFuelRoute = true;
            OnLowFuel?.Invoke(fuelType);
        }

        if (currentFuel <= 0f)
        {
            EngineOn = false;
            OnFuelDepleted?.Invoke();
        }
    }

    public bool AttemptStartRefuelling()
    {
        if (inPoliceChase && currentChaseRefuelAllowance > maximumChaseRefuelAllownace)
        {
            fuelStationsLockedDown = true;
            return false;
        }

        Refuelling = true;
        refuelTimer = RefuelDuration;
        return true;
    }

    public void ProcessRefuelling()
    {
        refuelTimer -= Time.deltaTime;

        if (refuelTimer <= 0f)
        {
            currentFuel = 100f;
            Refuelling = false;
            TriggerLowFuelRoute = false;

            if (inPoliceChase)
            {
                currentChaseRefuelAllowance++;
                if (currentChaseRefuelAllowance  >= maximumChaseRefuelAllownace)
                {
                    fuelStationsLockedDown = true;
                }
            }
        }
    }

    public void ResetCounter()
    {
        currentChaseRefuelAllowance = 0;
        fuelStationsLockedDown = false;
    }
}
