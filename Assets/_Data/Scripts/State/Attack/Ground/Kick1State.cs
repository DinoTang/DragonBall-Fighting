using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickState : BaseGroundKickState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 5;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new Kick2State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.BackMainState();
            }
        }
    }
}
