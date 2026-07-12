using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioSource radioSource;
    public AudioClip[] playlist;

    private int currentIndex = 0;

    public void Play()
    {
        if (playlist.Length == 0) return;

        radioSource.clip = playlist[currentIndex];
        radioSource.Play();
    }

    public void Stop()
    {
        radioSource.Stop();
    }

    public void NextTrack()
    {
        currentIndex = (currentIndex + 1) % playlist.Length;
        Play();
    }

    public AudioClip GetCurrentTrack()
    {
        return playlist.Length > 0 ? playlist[currentIndex] : null;
    }
}
