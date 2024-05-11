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
            redLights[redLights.Length + 1].gameObject.SetActive(true);
        }
        else if (reversed)
        {
            StartCoroutine("ReversedTraffic");
            greenLights[greenLights.Length + 1].gameObject.SetActive(true);
        }
    }

    public IEnumerator Traffic()
    {
        while (true)
        {
            redLights[0].gameObject.SetActive(true);
            redLights[1].gameObject.SetActive(true);
            red = true;
            yield return new WaitForSeconds(8);
            amberLights[0].gameObject.SetActive(true);
            amberLights[1].gameObject.SetActive(true);
            amber = true;
            yield return new WaitForSeconds(2);
            redLights[0].gameObject.SetActive(false);
            redLights[1].gameObject.SetActive(false);
            amberLights[0].gameObject.SetActive(false);
            amberLights[1].gameObject.SetActive(false);
            greenLights[0].gameObject.SetActive(true);
            greenLights[1].gameObject.SetActive(true);
            red = false;
            amber = false;
            green = true;
            yield return new WaitForSeconds(10);
            amberLights[0].gameObject.SetActive(true);
            amberLights[1].gameObject.SetActive(true);
            amber = true;
            green = false;
            greenLights[0].gameObject.SetActive(false);
            greenLights[1].gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            redLights[0].gameObject.SetActive(true);
            redLights[1].gameObject.SetActive(true);
            red = true;
            amberLights[0].gameObject.SetActive(false);
            amberLights[1].gameObject.SetActive(false);
            amber = false;
        }
    }

    public IEnumerator ReversedTraffic()
    {
        while (true)
        {
            greenLights[0].gameObject.SetActive(true);
            greenLights[1].gameObject.SetActive(true);
            green = true;
            yield return new WaitForSeconds(8);
            amberLights[0].gameObject.SetActive(true);
            amberLights[1].gameObject.SetActive(true);
            greenLights[0].gameObject.SetActive(false);
            greenLights[1].gameObject.SetActive(false);
            amber = true;
            green = false;
            yield return new WaitForSeconds(2);
            redLights[0].gameObject.SetActive(true);
            redLights[1].gameObject.SetActive(true);
            amberLights[0].gameObject.SetActive(false);
            amberLights[1].gameObject.SetActive(false);
            red = true;
            amber = false;
            yield return new WaitForSeconds(8);
            amberLights[0].gameObject.SetActive(true);
            amberLights[1].gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            amberLights[0].gameObject.SetActive(false);
            amberLights[1].gameObject.SetActive(false);
            redLights[0].gameObject.SetActive(false);
            redLights[1].gameObject.SetActive(false);
            green = true;
        }
    }
}
