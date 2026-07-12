using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    protected EnemyBaseState CurrentState;

    private void Start()
    {
        CurrentState = GetInitialState();
        CurrentState?.Enter();
    }

    private void Update()
    {
        CurrentState?.UpdateLogic();
    }

    private void FixedUpdate()
    {
        CurrentState?.UpdatePhysics();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        if (newState == null || newState == CurrentState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    protected virtual EnemyBaseState GetInitialState()
    {
        return null;
    }
}