using UnityEngine;
public class PauseSystemManager : MonoBehaviour
{
    public static bool isPaused {get; set;}
    public GameObject pauseMenu;
    public PlayerNexus brainHub;

    public void CheckPause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnSaveClicked()
    {
        if (brainHub != null)
        {
            brainHub.SaveGame();   
        }
    }

    public void OnLoadGame()
    {
        if (brainHub != null)
        {
            brainHub.LoadGame();

            ResumeGame();
        }
    }

    public void OnQuitApplication()
    {
        Application.Quit();
    }
}
