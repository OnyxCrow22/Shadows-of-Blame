using UnityEngine;

public class EnemyBaseState
{
    public string Name { get; protected set; }
    protected EnemyStateMachine enemyStateMachine;

    public EnemyBaseState(string name, EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.Name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
