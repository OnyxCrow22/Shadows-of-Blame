using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0, 24f)]
    public float currentTime;
    public float timeSpeed;
    public TextMeshProUGUI time;

    [Header("CurrentTime")]
    public string currentTimeString;

    [Header("Sun Settings")]
    public Light sunLight;
    public float sunPos = 1f;
    public float sunIntensity = 1f;
    public AnimationCurve sunIntensityMultipler;
    public AnimationCurve sunlightTemperatureCurve;
    public Volume sunVol;

    [Header("Moon Settings")]
    public Light moonLight;
    public float moonIntensity = 1f;
    public AnimationCurve moonIntensityMultipler;
    public AnimationCurve moonLightTemperatureCurve;
    public Volume moonVol;

    public bool isDay = true;
    public bool sunActive = true;
    public bool moonActive = true;

    [Header("Calendar Settings")]
    public int currentDayIndex = 0; // 0 = Monday, 6 = Sunday
    private string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimeText();
        CheckShadowStatus();
    }

    // Update is called once per frame
    void Update()
    {
        float previousTime = currentTime;
        currentTime += Time.deltaTime * timeSpeed;

        // Check if we passed midnight
        if (currentTime >= 24f)
        {
            currentTime -= 24f; // More accurate than setting to 0
            currentDayIndex++;

            if (currentDayIndex >= 7)
            {
                currentDayIndex = 0; // Reset to Monday
            }
        }

        UpdateTimeText();
        UpdateLight();
        CheckShadowStatus();
    }

    private void OnValidate()
    {
        UpdateLight();
        CheckShadowStatus();
    }

    void UpdateTimeText()
    {
        string timeString = Mathf.Floor(currentTime).ToString("00") + ":" + ((currentTime % 1 * 60)).ToString("00");
        // Append the day name to the UI string
        time.text = daysOfWeek[currentDayIndex] + " | " + timeString;
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        sunLight.transform.rotation = Quaternion.Euler(sunRotation - 90f, sunPos, 0f);
        moonLight.transform.rotation = Quaternion.Euler(sunRotation + 90f, sunPos, 0f);

        float normalizedTime = currentTime / 24f;
        float sunintensityCurve = sunIntensityMultipler.Evaluate(normalizedTime);
        float moonintensityCurve = moonIntensityMultipler.Evaluate(normalizedTime);

        if (sunLight != null)
        {
            sunLight.intensity = sunintensityCurve * sunIntensity;
        }

        if (moonLight != null)
        {
            moonLight.intensity = moonintensityCurve * moonIntensity;
        }

        float suntemperatureMultipler = sunlightTemperatureCurve.Evaluate(normalizedTime);
        float moonTemperatureMultipler = moonLightTemperatureCurve.Evaluate(normalizedTime);
        Light sunLightComponent = sunLight.GetComponent<Light>();
        Light moonLightComponent = moonLight.GetComponent<Light>();

        if (sunLightComponent != null)
        {
            sunLightComponent.colorTemperature = suntemperatureMultipler * 10000f;
        }

        if (moonLightComponent != null)
        {
            moonLightComponent.colorTemperature = moonTemperatureMultipler * 10000f;
        }
    }

    void CheckShadowStatus()
    {
        UniversalAdditionalLightData sunLightData = sunLight.GetComponent<UniversalAdditionalLightData>();
        UniversalAdditionalLightData moonLightData = moonLight.GetComponent<UniversalAdditionalLightData>();
        float currentSunRotation = currentTime;

        if (currentSunRotation >= 6f && currentSunRotation <= 18f)
        {
            // No shadows at night, but definitely during the day.
            sunLight.shadows = LightShadows.Soft;
            moonLight.shadows = LightShadows.None;
            isDay = true;
        }
        else
        {
            // Soft shadows at night, none for the sun.
            sunLight.shadows = LightShadows.None;
            moonLight.shadows = LightShadows.Soft;
            isDay = false;
        }
        if (currentSunRotation >= 6f && currentSunRotation <= 18f)
        {
            sunVol.gameObject.SetActive(true);
            sunLight.gameObject.SetActive(true);
            sunActive = true;
        }
        else
        {
            sunVol.gameObject.SetActive(false);
            sunLight.gameObject.SetActive(false);
            sunActive = false;
        }
        if (currentSunRotation >= 6f && currentSunRotation <= 18f)
        {
            moonVol.gameObject.SetActive(false);
            moonLight.gameObject.SetActive(false);
            moonActive = false;
        }
        else
        {
            moonVol.gameObject.SetActive(true);
            moonLight.gameObject.SetActive(true);
            moonActive = true;
        }
    }
}
