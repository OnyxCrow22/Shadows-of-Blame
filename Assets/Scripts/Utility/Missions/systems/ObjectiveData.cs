using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveState
{
    InActive,
    Active,
    Completed,
    Failed,
    LockedOut,
    None,
}

[System.Serializable]
public struct ObjectiveData
{
    public string objectiveID;
    public string description;
    public List<string> requirements;
    public List<string> exclusivity; // Exclusive paths
    List<string> objectiveBypass; // Objectives that can be bypassed.
    public bool isOptional;
}
