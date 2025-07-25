using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick3State : BaseKickState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 7;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= duration)
        {
            if (this.shouldCombo && this.isHitting)
            {
                this.stateManager.SetNextState(new Kick4State());
            }
            else
            {
                this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
                this.BackMainState();
            }
        }
    }
}
