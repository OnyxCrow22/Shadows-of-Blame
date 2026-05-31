using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStoppage : MonoBehaviour
{
    public void TimeRestart()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}
