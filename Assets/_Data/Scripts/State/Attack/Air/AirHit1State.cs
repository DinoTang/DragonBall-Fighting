using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit1State : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 1;
        base.OnEnter(stateManager);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // if (this.timed > duration * 0.3f && InputManager.Instance.GetKickInput()) this.otherAttack = true;

        if (this.timed < this.duration) return;

        if (this.shouldCombo && this.isHitting)
        {
            if (this.otherAttack) this.stateManager.SetNextState(new AirKickState());
            else this.stateManager.SetNextState(new AirHit2State());
        }
        else
        {
            this.stateManager.CharacterCtrl.CharacterDamageSender.SetIsHitting(false);
            this.stateManager.SetNextStateToMain();
        }
    }
}
