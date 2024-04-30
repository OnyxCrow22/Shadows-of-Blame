using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool reversed = false;
    public Light redLights;
    public Light greenLights;
    public Light amberLights;
    public bool green, amber, red;

    private void Start()
    {
        if (!reversed)
        {
            StartCoroutine("Traffic");
            redLights.gameObject.SetActive(true);
        }
        if (reversed)
        {
            StartCoroutine("ReversedTraffic");
            greenLights.gameObject.SetActive(true);
        }
    }

    public IEnumerator Traffic()
    {
        while (true)
        {
            redLights.gameObject.SetActive(true);
            red = true;
            yield return new WaitForSeconds(8);
            amberLights.gameObject.SetActive(true);
            amber = true;
            yield return new WaitForSeconds(2);
            redLights.gameObject.SetActive(false);
            amberLights.gameObject.SetActive(false);
            greenLights.gameObject.SetActive(true);
            red = false;
            amber = false;
            green = true;
            yield return new WaitForSeconds(10);
            amberLights.gameObject.SetActive(true);
            amber = true;
            green = false;
            greenLights.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            redLights.gameObject.SetActive(true);
            red = true;
            amberLights.gameObject.SetActive(false);
            amber = false;
        }
    }

    public IEnumerator ReversedTraffic()
    {
        while (true)
        {
            greenLights.gameObject.SetActive(true);
            green = true;
            yield return new WaitForSeconds(10);
            amberLights.gameObject.SetActive(true);
            greenLights.gameObject.SetActive(false);
            amber = true;
            green = false;
            yield return new WaitForSeconds(2);
            redLights.gameObject.SetActive(true);
            amberLights.gameObject.SetActive(false);
            red = true;
            amber = false;
            yield return new WaitForSeconds(8);
            amberLights.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            amberLights.gameObject.SetActive(false);
            redLights.gameObject.SetActive(false);
            greenLights.gameObject.SetActive(true);
            amber = false;
            red = false;
            green = true;
        }
    }
}
