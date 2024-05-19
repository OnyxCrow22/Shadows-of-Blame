using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCall : MonoBehaviour
{
    public Lift liftToCall;

    public void Up()
    {
        StartCoroutine(liftToCall.OperateLift());
    }

    public void Down()
    {
        StartCoroutine(liftToCall.GoingDown());
    }
}
