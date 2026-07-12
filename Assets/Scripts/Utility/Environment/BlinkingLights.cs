using System.Collections;
using UnityEngine;

public class BlinkingLights : MonoBehaviour
{
    public enum LightType { Skyscraper, Airfield }
    public LightType mode;
    public GameObject[] lights;
    public float delay = 0.5f;

    private void Start()
    {
        StartCoroutine(ManageLights());
    }

    private IEnumerator ManageLights()
    {
        while (true)
        {
            switch (mode)
            {
                case LightType.Skyscraper:
                    yield return StartCoroutine(PulseBeacon());
                    break;
                case LightType.Airfield:
                    yield return StartCoroutine(RunRunwaySequence());
                    break;
            }
        }
    }

    private IEnumerator PulseBeacon()
    {
        foreach (GameObject light in lights) light.SetActive(true);
        yield return new WaitForSeconds(delay);
        foreach (GameObject light in lights) light.SetActive(false);
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator RunRunwaySequence()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            light.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
    }
}