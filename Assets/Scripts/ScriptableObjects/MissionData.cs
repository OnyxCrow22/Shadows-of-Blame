using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MissionData : ScriptableObject
{
    public string missionTitle;
    public List<string> objectiveDescriptions;
}