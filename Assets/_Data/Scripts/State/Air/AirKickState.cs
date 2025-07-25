using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKickState : BaseAirHitState
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
            this.BackMainState();
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
        }
    }
}
