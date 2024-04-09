using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject MainUI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject audioSettings;
    public GameObject instructionsPanel;
    public bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        MainUI.SetActive(false);
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        MainUI.SetActive(true);
        paused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Audio()
    {
        settingsMenu.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void Instructions()
    {
        pauseMenu.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("FrontEnd");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
