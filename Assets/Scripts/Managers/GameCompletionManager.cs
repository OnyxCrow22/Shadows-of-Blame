using System;
using UnityEngine;

public class GameCompletionManager : MonoBehaviour
{
    public static event Action OnSandboxMode;
    public static event Action OnGameRestart;

    public void EnterSandboxMode()
    {
        OnSandboxMode?.Invoke();
    }

    public void RestartGame()
    {
        OnGameRestart?.Invoke();
    }
}
