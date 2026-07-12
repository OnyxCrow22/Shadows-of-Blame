using UnityEngine;
using System;
using System.Collections;

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
