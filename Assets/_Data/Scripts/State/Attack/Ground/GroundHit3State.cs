using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit3State : BaseGroundHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 3;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            if (shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new GroundHit4State());
            }
            else
            {
                this.BackMainState();
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
            }
        }
    }



}
