using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLights : MonoBehaviour
{
    public GameObject[] lights;

    private void Start()
    {
        StartCoroutine("AircraftLights");
        lights[lights.Length - 1].SetActive(true);
    }

    public IEnumerator AircraftLights()
    {
        while (true)
        {
            lights[lights.Length - 1].SetActive(true);
            yield return new WaitForSeconds(1);
            lights[lights.Length - 1].SetActive(false);
        }
    }
}
