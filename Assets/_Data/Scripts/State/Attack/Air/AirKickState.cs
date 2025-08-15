using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKickState : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 4;
        base.OnEnter(stateManager); 
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed < this.duration) return;

        if (this.shouldCombo && this.isHitting)
        {
            this.stateManager.SetNextState(new AirHit1State());
        }
        else
        {
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
            this.stateManager.SetNextStateToMain();
        }
    }
}
