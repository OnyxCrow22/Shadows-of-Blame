using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Slider sunBrightnessSlider, moonBrightnessSlider;
    public TextMeshProUGUI sunBrightnessValue;
    public TextMeshProUGUI moonBrightnessValue;
    public Light sunlightIntenstity;
    public Light moonLightIntenstity;

    private void Awake()
    {
        sunBrightnessSlider.onValueChanged.AddListener(SetSunIntensity);
        moonBrightnessSlider.onValueChanged.AddListener(SetMoonIntensity);
    }

    private void Start()
    {
        // Load values
        sunBrightnessSlider.value = PlayerPrefs.GetFloat("sunBrightness", 1f);
        moonBrightnessSlider.value = PlayerPrefs.GetFloat("moonBrightness", 1f);

        // Explicitly update lights with the loaded values
        SetSunIntensity(sunBrightnessSlider.value);
        SetMoonIntensity(moonBrightnessSlider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("sunBrightness", sunBrightnessSlider.value);
        PlayerPrefs.SetFloat("moonBrightness", moonBrightnessSlider.value);
    }

    public void SetSunIntensity(float value)
    {
        sunlightIntenstity.intensity = value; // Update the light
        sunBrightnessValue.text = value.ToString("0.00"); // Update the UI
        PlayerPrefs.SetFloat("sunBrightness", value); // Save immediately
    }

    public void SetMoonIntensity(float value)
    {
        moonLightIntenstity.intensity = value;
        moonBrightnessValue.text = value.ToString("0.00");
        PlayerPrefs.SetFloat("moonBrightness", value);
    }
}
