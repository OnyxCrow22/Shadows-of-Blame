using UnityEngine;

public class NPCBaseState
{
    public string name;
    protected NPCStateMachine npcStateMachine;

    public NPCBaseState(string name, NPCStateMachine npcStateMachine)
    {
        this.npcStateMachine = npcStateMachine;
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
