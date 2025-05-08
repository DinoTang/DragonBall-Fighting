using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit4State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 4;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            this.BackMainState();
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitSuccessful(false);
        }
    }

}
