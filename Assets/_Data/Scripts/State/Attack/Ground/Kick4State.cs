using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick4State : BaseGroundKickState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 8;
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
