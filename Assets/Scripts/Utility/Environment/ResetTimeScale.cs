using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimeScale : MonoBehaviour
{
    public void ResetTime()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}
