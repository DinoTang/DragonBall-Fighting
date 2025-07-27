using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit1State : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 1;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= this.duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new AirHit2State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.stateManager.SetNextStateToMain();
            }
        }
    }
}
