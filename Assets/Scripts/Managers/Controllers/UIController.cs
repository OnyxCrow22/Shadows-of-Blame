using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject audioSettings;
    public GameObject graphicsPanel;
    public GameObject instructionsPanel;

    public RegionDisplayUI regions; // A new holder for regional scripts.

    

    private void OnEnable()
    {
        GameStateManager.OnPaused += ShowPauseMenu;
        GameStateManager.OnResumed += HidePauseMenu;
    }

    private void OnDisable()
    {
        GameStateManager.OnPaused -= ShowPauseMenu;
        GameStateManager.OnResumed -= HidePauseMenu;
    }

    private void ShowPauseMenu()
    {
        mainUI.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        mainUI.SetActive(true);
    }

    public void OpenSettings() => SwitchScreen(settingsMenu);
    public void OpenAudio() => SwitchScreen(audioSettings);
    public void OpenGraphics() => SwitchScreen(graphicsPanel);
    public void OpenInstructions() => SwitchScreen(instructionsPanel);

    private void SwitchScreen(GameObject screen)
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        audioSettings.SetActive(false);
        graphicsPanel.SetActive(false);
        instructionsPanel.SetActive(false);

        screen.SetActive(true);
    }

    public void DisplayRegion(RegionData region)
    {
        if (regions != null)
        {
            regions.DisplayRegion(region);
        }
    }

    public void ClearRegionDisplay()
    {
        if (regions != null)
        {
            regions.ClearRegionDisplay();
        }
    }
}
