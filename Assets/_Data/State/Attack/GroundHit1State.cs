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
            if (this.shouldCombo && this.isHitSuccessful)
            {
                this.stateManager.SetNextState(new GroundHit2State());
            }
            else
            {
                this.stateManager.CharacterCtrl.DamageSender.SetIsHitSuccessful(false);
                this.BackMainState();
            }
        }
    }
}
