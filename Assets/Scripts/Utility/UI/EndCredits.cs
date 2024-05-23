using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public GameObject endCredits;
    public GameObject gameComplete;
    public AudioSource endMusic;
    public WestralWoes WW;

    public void CheckEvidence()
    {
        if (WW.place.EvidencePlaced)
        {
            StartCoroutine("Credits");
        }
    }
    public IEnumerator Credits()
    {
        endMusic.ignoreListenerPause = true;
        AudioListener.pause = true;
        yield return new WaitForSecondsRealtime(100);
        endCredits.SetActive(false);
        gameComplete.SetActive(true);
        endMusic.Stop();
        AudioListener.pause = false;
    }
}
