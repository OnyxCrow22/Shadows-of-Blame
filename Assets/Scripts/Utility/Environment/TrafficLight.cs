using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool reversed = false;
    public Light[] redLights;
    public Light[] greenLights;
    public Light[] amberLights;
    public bool green, amber, red;

    private void Start()
    {
        if (!reversed)
        {
            StartCoroutine("Traffic");
            Lights(redLights, true);
        }
        else if (reversed)
        {
            StartCoroutine("ReversedTraffic");
            Lights(greenLights, true);
        }
    }

    public IEnumerator Traffic()
    {
        while (true)
        {
            Lights(redLights, true);
            red = true;
            yield return new WaitForSeconds(8);
            Lights(amberLights, true);
            amber = true;
            yield return new WaitForSeconds(2);
            Lights(redLights, false);
            Lights(amberLights, false);
            Lights(greenLights, true);
            red = false;
            amber = false;
            green = true;
            yield return new WaitForSeconds(10);
            Lights(amberLights, true);
            amber = true;
            green = false;
            Lights(greenLights, false);
            yield return new WaitForSeconds(2);
            Lights(redLights, true);
            red = true;
            Lights(amberLights, false);
            amber = false;
        }
    }

    public IEnumerator ReversedTraffic()
    {
        while (true)
        {
            Lights(greenLights, true);
            green = true;
            yield return new WaitForSeconds(10);
            Lights(amberLights, true);
            Lights(greenLights, false);
            amber = true;
            green = false;
            yield return new WaitForSeconds(2);
            Lights(redLights, true);
            Lights(amberLights, false);
            red = true;
            amber = false;
            yield return new WaitForSeconds(8);
            Lights(amberLights, true);
            yield return new WaitForSeconds(2);
            Lights(amberLights, false);
            Lights(redLights, false);
            Lights(greenLights, true);
            green = true;
        }
    }

    public void Lights(Light[] trafficLight, bool isActive)
    {
        for (int i = 0; i < trafficLight.Length; i++)
        {
            trafficLight[i].gameObject.SetActive(isActive);
        }
    }
}
