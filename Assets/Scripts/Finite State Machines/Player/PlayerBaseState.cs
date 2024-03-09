using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState
{
    public string name;
    protected PlayerStateMachine psm;

    public PlayerBaseState(string name, PlayerStateMachine psm)
    {
        this.psm = psm;
        this.name = name;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
