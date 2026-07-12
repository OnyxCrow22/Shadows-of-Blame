using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public static event Action OnPaused;
    public static event Action OnResumed;

    public static bool IsPaused { get; private set; }

    public static void Pause()
    {
        if (IsPaused) return;

        IsPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;

        OnPaused?.Invoke();
    }

    public static void Resume()
    {
        if (!IsPaused) return;

        IsPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;

        OnResumed?.Invoke();
    }
}
