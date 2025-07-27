using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit2State : BaseGroundHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 2;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            if (shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new GroundHit3State());
            }
            else
            {
                this.BackMainState();
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
            }
        }
    }
}
