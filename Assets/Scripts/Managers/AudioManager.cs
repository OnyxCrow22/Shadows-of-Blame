using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;
    public AudioMixer master;
    public Sound[] sounds;

    public const string MIXER_MUSIC = "MusicVol";
    public const string MIXER_SFX = "SFXVol";

    public const string MUSIC_PREF = "MusicVolPref";
    public const string SFX_PREF = "SFXVolPref";

    private Dictionary<string, Sound> soundDict;

    public const string MUSIC_KEY = "MusicVol";
    public const string SFX_KEY = "SFXVol";

    void Awake()
    {
        if (manager == null) manager = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
        InitializeAudio();
        LoadVol();
    }

    private void InitializeAudio()
    {
        soundDict = new Dictionary<string, Sound>();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.group;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            // Prevent duplicate key errors if the inspector has typos
            if (!soundDict.ContainsKey(s.name))
                soundDict.Add(s.name, s);
        }
    }

    public void Play(string name)
    {
        if (soundDict.TryGetValue(name, out Sound s))
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound: {name} was not found!");
        }
    }

    public void Stop(string name)
    {
        if (soundDict.TryGetValue(name, out Sound s))
        {
            s.source.Stop();
        }
    }

    public void LoadVol()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_PREF, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_PREF, 1f);

        master.SetFloat(MIXER_MUSIC, Mathf.Log10(Mathf.Max(musicVolume, 0.0001f)) * 20);
        master.SetFloat(MIXER_SFX, Mathf.Log10(Mathf.Max(sfxVolume, 0.0001f)) * 20);
    }
}