using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit1State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 1;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            if (shouldCombo)
            {
                this.stateManager.SetNextState(new GroundHit2State());
            }
            else
            {
                this.BackMainState();
            }
        }
    }
}
