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
        // if (this.timed > duration * 0.3f && InputManager.Instance.GetHitInput()) this.otherAttack = true;

        if (this.timed < this.duration) return;

        if (this.otherAttack) this.stateManager.SetNextState(new AirHit1State());
        else
        {
            this.BackMainState();
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
        }

    }
}
