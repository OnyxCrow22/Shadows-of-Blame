using System;

public static class CutsceneEvents
{
    // Fired when any cutscene finishes
    public static event Action OnCutsceneFinished;

    public static void RaiseCutsceneFinished()
    {
        OnCutsceneFinished?.Invoke();
    }
}
