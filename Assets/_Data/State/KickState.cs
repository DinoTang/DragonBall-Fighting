using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickState : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 5;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {

            this.BackMainState();
        }
    }
}
