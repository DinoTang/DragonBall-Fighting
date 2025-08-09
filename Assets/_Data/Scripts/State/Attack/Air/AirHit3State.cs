using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit3State : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 3;
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsKnockingDown(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed < this.duration) return;

        this.BackMainState();
        this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
    }
}
