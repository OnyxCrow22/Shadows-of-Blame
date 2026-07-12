using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name; // The name of the clip
    public AudioClip clip; // The clip required
    public AudioMixerGroup group; // The AudioMixerGroup required
    public bool loop; 

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float volume = 1f; // Initialized to default 1.0f
    [Range(0.1f, 3f)] public float pitch = 1f; // Initialized to default 1.0f

    [Header("3D Spatial Settings")]
    [Tooltip("0 = Fully 2D (Global/UI), 1 = Fully 3D (World Space Positioning)")]
    [Range(0f, 1f)] public float spatialBlend = 0f;
    public float minDistance = 1f;
    public float maxDistance = 20f;
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

    [HideInInspector] public AudioSource source;

    /// <summary>
    /// Configures an AudioSource component with all specified sound data properties.
    /// </summary>
    public void ConfigureSource(AudioSource targetSource)
    {
        source = targetSource; 
        source.clip = clip; 
        source.outputAudioMixerGroup = group; 
        source.loop = loop; 
        source.volume = volume; 
        source.pitch = pitch; 

        // Apply spatial properties
        source.spatialBlend = spatialBlend;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = rolloffMode;
    }
}