using UnityEngine;
using TMPro;
using System.Collections;

public class VehicleUIController : MonoBehaviour
{
    [Header("Speedometer")]
    public TextMeshProUGUI speedText;
    public RectTransform needle;
    public float minNeedleAngle = -130f;
    public float maxNeedleAngle = 130f;
    public float maxSpeedMPH = 160f;

    [Header("Lights")]
    public Light leftIndicator;
    public Light rightIndicator;
    public Light[] reverseLights;
    public Light[] brakeLights;

    private Coroutine leftRoutine;
    private Coroutine rightRoutine;

    public void UpdateSpeed(float mph)
    {
        speedText.text = $"{mph:00} MPH";

        float t = Mathf.Clamp01(mph / maxSpeedMPH);
        float angle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, t);

        needle.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void SetBrakeLights(bool active)
    {
        foreach (var light in brakeLights)
            light.enabled = active;
    }

    public void SetReverseLights(bool active)
    {
        foreach (var light in reverseLights)
            light.enabled = active;
    }

    public void ToggleLeftIndicator(bool active, float blinkRate)
    {
        if (active)
        {
            if (leftRoutine == null)
                leftRoutine = StartCoroutine(Blink(leftIndicator, blinkRate));
        }
        else
        {
            if (leftRoutine != null)
            {
                StopCoroutine(leftRoutine);
                leftRoutine = null;
            }
            leftIndicator.enabled = false;
        }
    }

    public void ToggleRightIndicator(bool active, float blinkRate)
    {
        if (active)
        {
            if (rightRoutine == null)
                rightRoutine = StartCoroutine(Blink(rightIndicator, blinkRate));
        }
        else
        {
            if (rightRoutine != null)
            {
                StopCoroutine(rightRoutine);
                rightRoutine = null;
            }
            rightIndicator.enabled = false;
        }
    }

    private IEnumerator Blink(Light light, float rate)
    {
        while (true)
        {
            light.enabled = !light.enabled;
            yield return new WaitForSeconds(rate);
        }
    }
}
