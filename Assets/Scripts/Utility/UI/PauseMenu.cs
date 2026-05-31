using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject MainUI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject audioSettings;
    public GameObject instructionsPanel;
    public GameObject graphicsPanel;
    
    [HideInInspector] public bool paused = false;

    [Header("Input References")]
    [SerializeField] private PlayerInput pInput;

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (!paused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void OnResume(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }


    public void PauseGame()
    {
        MainUI.SetActive(false);
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
            
        pInput?.SwitchCurrentActionMap("UI");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        MainUI.SetActive(true);
        paused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;

        pInput?.SwitchCurrentActionMap("Movement");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    /*
    public void ReturnToPause()
    {
        settingsMenu.SetActive(false);
        audioSettings.SetActive(false);
        graphicsPanel.SetActive(false);
        instructionsPanel.SetActive(false);

        pauseMenu.SetActive(true);
    }
    */

    public void Instructions()
    {
        pauseMenu.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void Graphics()
    {
        settingsMenu.SetActive(false);
        graphicsPanel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("FrontEnd");
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
