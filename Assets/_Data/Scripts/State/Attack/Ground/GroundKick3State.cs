using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundKick3State : BaseGroundKickState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 7;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new GroundKick4State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.BackMainState();
            }
        }
    }
}
