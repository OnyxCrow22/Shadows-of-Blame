using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("World Progression")]
    public bool HasWesteriaAccess = false;
    public bool HasWestralAccess = false;
    public bool HasMelasaAccess = false;
    public bool IsInWestInsbury = false;

    [Header("Player Status")]
    public bool IsDead = false;
    public bool IsInVehicle = false;
    public bool IsInCutscene = false;
    public bool IsAiming = false;

    [Header("Mission Flags")]
    public bool CanRespawn = true;
    public bool IsInMission = false;

    // Called by VehicleEntrySystem
    public void SetPlayerInsideVehicle(bool inside)
    {
        IsInVehicle = inside;
    }

    // Called by RespawnManager
    public void SetDead(bool dead)
    {
        IsDead = dead;
    }

    public void SetRespawnAllowed(bool allowed)
    {
        CanRespawn = allowed;
    }

    public void SetMissionState(bool inMission)
    {
        IsInMission = inMission;
    }

    public void SetAiming(bool aiming)
    {
        IsAiming = aiming;
    }
}
