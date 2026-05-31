using UnityEngine;

public class EnemyBaseState
{
    public string name;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyBaseState(string name, EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
