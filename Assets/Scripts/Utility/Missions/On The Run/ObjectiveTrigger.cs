using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveActivate : MonoBehaviour
{
    [Header("Data")]
    public string requiredID; // What is required to progress
    public string objectiveID; // What causes the objective to progress

    [Header("Visuals")]
    public GameObject beforeState; // What it looks like before the trigger is met
    public GameObject afterState; // What the object looks like after the trigger is met
    public bool useCutscene = true;

    [Header("Events")]
    public static UnityEvent<string> OnObjectiveComplete = new(); // Create a new event on ObjectiveComplete
    public UnityEvent onTriggerActivated; // A new event to control a trigger

    private bool hasActivated = false;
    private CancellationTokenSource cts;

    void OnEnable()
    {
        cts = new CancellationTokenSource();
    }

    void OnDisable()
    {
        cts?.Cancel();
        cts?.Dispose();
    }

    public void OnInteract()
    {
        if (hasActivated) // ||  !InventoryManager.Instance.HasItem(requiredID)) return;

        hasActivated = true;
        _ = ActivateSequenceAsync(cts.Token);
    }

    public async Task ActivateSequenceAsync(CancellationToken token)
    {
        hasActivated = true;
        onTriggerActivated?.Invoke();

        try
        {
            if (useCutscene)
            {
                // await CutsceneManager.Instance.PlayAsync("EvidencePlace_Fade", token); // Activate the cutscene
            }
            else
            {
                await Task.Delay(500, token); // 0.5 milliseconds
            }

            beforeState.SetActive(false);
            afterState.SetActive(true);

            OnObjectiveComplete?.Invoke(objectiveID);
        }
        catch (OperationCanceledException)
        {
            hasActivated = false; // Allow retry
            Debug.Log("The ObjectiveTrigger sequence was cancelled!");
        }
    }
}
