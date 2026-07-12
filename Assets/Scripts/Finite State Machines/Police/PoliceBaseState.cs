using UnityEngine;

public class PoliceBaseState
{
    public string Name { get; protected set; }
    protected PoliceStateMachine policeMachine;

    public PoliceBaseState(string name, PoliceStateMachine policeMachine)
    {
        this.policeMachine = policeMachine;
        this.Name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
