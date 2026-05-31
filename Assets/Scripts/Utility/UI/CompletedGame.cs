using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedGame : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject gameComplete;
    public PlayerMovementSM playsm;
    public WestralWoes WW;

    public void SandboxMode()
    {
        MainUI.SetActive(true);
        gameComplete.SetActive(false);
        playsm.GetComponent<PlayerMovementSM>().enabled = true;
        WW.missionName.text = "";
        WW.locationClues[0].text = "";
        WW.locationClues[1].text = "";
        WW.locationClues[2].text = "";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("ShadowsOfBlame");
    }
}
