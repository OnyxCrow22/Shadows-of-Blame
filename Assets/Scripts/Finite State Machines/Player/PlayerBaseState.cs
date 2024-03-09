using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState
{
    public string name;
    protected PlayerStateMachine playerStateMachine;

    public PlayerBaseState(string name, PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
