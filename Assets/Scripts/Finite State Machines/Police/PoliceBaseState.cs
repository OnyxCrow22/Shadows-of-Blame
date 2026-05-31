using UnityEngine;

public class PoliceBaseState
{
    public string name;
    protected PoliceStateMachine policeMachine;

    public PoliceBaseState(string name, PoliceStateMachine policeMachine)
    {
        this.policeMachine = policeMachine;
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
