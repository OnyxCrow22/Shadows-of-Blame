using UnityEngine;
using TMPro;
using System; // Assuming you are using TextMeshPro for your UI

public class MissionManager : MonoBehaviour
{
    public MissionData currentMission;
    public TextMeshProUGUI objectiveText; // Reference to your UI
    private int currentStep = 0;

    void OnEnable()
    {
        MissionEvents.OnObjectiveComplete += AdvanceMission;
        // Optional: Listen for other events like death or collection
        MissionEvents.OnGangLeaderKilled += HandleLeaderKilled;
    }

    private void AdvanceMission(bool obj)
    {
        throw new NotImplementedException();
    }

    void OnDisable()
    {
        MissionEvents.OnObjectiveComplete -= AdvanceMission;
        MissionEvents.OnGangLeaderKilled -= HandleLeaderKilled;
    }

    void AdvanceMission(string objectiveID)
    {
        // Only advance if the ID matches what the mission expects
        if (currentStep < currentMission.objectiveDescriptions.Count - 1)
        {
            currentStep++;
            UpdateUI(currentMission.objectiveDescriptions[currentStep]);
        }
    }

    void UpdateUI(string text)
    {
        if (objectiveText != null)
            objectiveText.text = text;
    }

    void HandleLeaderKilled()
    {
        // Logic for handling the death event
        Debug.Log("Leader killed, updating mission state...");
    }
}