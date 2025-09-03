using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundKick2State : BaseGroundKickState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 6;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new GroundKick3State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.BackMainState();
            }
        }
    }
}
