using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit3State : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 3;
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsKnockdown(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.fixedTime >= this.duration)
        {
            this.BackMainState();
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
        }
    }
}
