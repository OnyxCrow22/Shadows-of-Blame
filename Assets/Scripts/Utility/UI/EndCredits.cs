using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject endCredits;
    public GameObject gameComplete;
    public AudioSource endMusic;
    public IEnumerator Credits()
    {
        endMusic.ignoreListenerPause = true;
        AudioListener.pause = true;
        MainUI.SetActive(false);
        endCredits.SetActive(true);
        yield return new WaitForSeconds(100);
        MainUI.SetActive(true);
        endCredits.SetActive(false);
        gameComplete.SetActive(true);
        endMusic.Stop();
        AudioListener.pause = false;
    }
}
