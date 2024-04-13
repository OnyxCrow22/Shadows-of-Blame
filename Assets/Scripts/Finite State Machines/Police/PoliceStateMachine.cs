using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStateMachine : MonoBehaviour
{
    PoliceBaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(PoliceBaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual PoliceBaseState GetInitialState()
    {
        return null;
    }
}
