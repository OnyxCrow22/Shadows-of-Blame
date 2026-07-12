using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (GameStateManager.IsPaused)
            GameStateManager.Resume();
        else
            GameStateManager.Pause();
    }
}
