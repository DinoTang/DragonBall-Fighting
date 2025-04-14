using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherState : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 6;
        base.OnEnter(stateManager);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Fixedtime >= 0.25)
        {
            this.BackMainState();
        }
    }
}
