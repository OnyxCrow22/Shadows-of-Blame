using UnityEngine;
using System;
using System.Collections;

// Responsible for playing the credits sequence and notifying other systems when it starts and ends.
public class CreditsManager : MonoBehaviour
{
    public static event Action OnCreditsStarted;
    public static event Action OnCreditsFinished;

    public float creditsDuration = 100f;

    public void StartCredits()
    {
        OnCreditsStarted?.Invoke();
        StartCoroutine(CreditsSequence());
    }

    private IEnumerator CreditsSequence()
    {
        yield return new WaitForSecondsRealtime(creditsDuration);
        OnCreditsFinished?.Invoke();
    }
}
