using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShadowsOfBlame/Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionTitle;
    public List<string> objectiveDescriptions;
}