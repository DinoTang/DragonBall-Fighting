using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit4State : BaseGroundHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 4;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            this.BackMainState();
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
        }
    }

}
