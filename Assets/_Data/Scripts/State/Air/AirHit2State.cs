using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit2State : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 2;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.fixedTime >= this.duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new AirHit3State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.stateManager.SetNextStateToMain();
            }
        }
    }
}
