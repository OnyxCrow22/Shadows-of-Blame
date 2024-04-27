using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWWesteriaPlayerHome : MonoBehaviour
{
    public bool nowHome = false;
    public WestralWoes WW;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
        {
            nowHome = true;
            WW.backHome = true;
            WW.objective.text = "";
        }
    }
}
