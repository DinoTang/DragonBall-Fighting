using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUpState : CharacterState
{
    protected float knockDuration = 0.5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsKnockUp", true);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsKnockUp(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.knockDuration)
        {
            if (this.stateManager.CharacterCtrl.CharacterDamageReceiver.IsHurt && this.stateManager.CharacterCtrl.Opponent.CharacterDamageSender.IsHitting) return;
            int dir = this.stateManager.CharacterCtrl.CharacterFlip.Direct * -1;
            this.stateManager.CharacterCtrl.Rgb.linearVelocity = new Vector2(dir * 10f, 30f);
            this.stateManager.SetNextStateToMain();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockUp", false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsKnockUp(false);
    }
}
