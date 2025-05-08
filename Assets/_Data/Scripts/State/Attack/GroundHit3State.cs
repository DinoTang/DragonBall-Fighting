using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit3State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 3;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            if (shouldCombo && this.isHitSuccessful)
            {
                this.stateManager.SetNextState(new GroundHit4State());
            }
            else
            {
                this.BackMainState();
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitSuccessful(false);
            }
        }
    }



}
