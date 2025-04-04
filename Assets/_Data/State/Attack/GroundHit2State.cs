using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit2State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 2;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            if (shouldCombo)
            {
                this.stateManager.SetNextState(new GroundHit3State());
            }
            else
            {
                this.BackMainState();
            }
        }
    }
}
