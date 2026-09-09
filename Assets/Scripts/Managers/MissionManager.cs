using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public MissionData currentMission;
    public TextMeshProUGUI objectiveText;
    private HashSet<string> activeObjective; // Current objective.
    private HashSet<string> objectivesCompleted; // Store the objectives in a hashset.
    private HashSet<string> failedObjectives; // Objectives that failed.
    public string currentObjective;
}