using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarSoundtrack : MonoBehaviour
{
    public string[] trackNames;
    public AudioClip[] songs;
    AudioClip currentSong;
    public AudioSource carRadio;
    public PlayerMovementSM playsm;
    public float currentTrack;
    int songSelect;
    public TextMeshProUGUI songName;
    bool canPlayRadio = false;

    private void Update()
    {
        if (playsm.inVehicle && Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(PlaySong());
            canPlayRadio = true;
        }

        if (playsm.inVehicle && Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(SkipSong());
        }

        if (playsm.inVehicle && Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(StopSong());
        }

        else if (!playsm.inVehicle)
        {
            StartCoroutine(StopSong());
        }
    }

    IEnumerator PlaySong()
    {
        List<int> playedSongs = new List<int>();
        int songCount = 0;
        while (songCount < 7)
        {
            do
            {
                songSelect = Random.Range(0, songs.Length);
            } while (playedSongs.Contains(songSelect));
            playedSongs.Add(songSelect);
            currentSong = songs[songSelect];
            carRadio.PlayOneShot(currentSong);
            trackNames[songSelect] = currentSong.name;
            songName.text = currentSong.name;
            yield return new WaitForSeconds(currentSong.length);
            songCount++;
        }
        if (songCount > 7)
        {
            yield break;
        }
    }

    IEnumerator SkipSong()
    {
        carRadio.Stop();
        songName.text = "";
        songSelect = Random.Range(0, songs.Length);
        currentSong = songs[songSelect];
        carRadio.PlayOneShot(currentSong);
        songName.text = currentSong.name;
        yield break;
    }

    IEnumerator StopSong()
    {
        carRadio.Stop();
        songName.text = "";
        yield break;
    }
}
