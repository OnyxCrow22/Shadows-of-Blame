using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio Configurations")]
    [SerializeField] private AudioMixer master;

    private string mixerMusicParam = AudioManager.MIXER_MUSIC;
    private string mixerSfxParam = AudioManager.MIXER_SFX;

    [Header("UI Components")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicVolText;
    [SerializeField] private TextMeshProUGUI sfxVolText;

    private void OnEnable()
    {
        // Register UI listeners safely when the menu becomes active
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        // Retrieve stored volume properties, defaulting to a full 1.0f volume allocation
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_PREF, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_PREF, 1f);

        // Force initialize UI text display readouts on first run
        UpdateSliderText(musicVolText, musicSlider.value);
        UpdateSliderText(sfxVolText, sfxSlider.value);
    }

    private void OnDisable()
    {
        // Unregister listeners to eliminate lingering background memory allocation references
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);

        // Save slider records safely to disk layout storage structures
        PlayerPrefs.SetFloat(AudioManager.MUSIC_PREF, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_PREF, sfxSlider.value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        CalculateVolume(mixerMusicParam, value);
        UpdateSliderText(musicVolText, value);
    }

    public void SetSFXVolume(float value)
    {
        CalculateVolume(mixerSfxParam, value);
        UpdateSliderText(sfxVolText, value);
    }

    private void CalculateVolume(string parameterName, float sliderValue)
    {
        // Clamp minimum value to 0.0001f to prevent a mathematical Log10(0) negative infinity error
        float safeValue = Mathf.Max(sliderValue, 0.0001f);
        float decibels = Mathf.Log10(safeValue) * 20f;

        master.SetFloat(parameterName, decibels);
    }

    private void UpdateSliderText(TextMeshProUGUI textComponent, float value)
    {
        if (textComponent != null)
        {
            textComponent.text = value.ToString("0.00");
        }
    }
}