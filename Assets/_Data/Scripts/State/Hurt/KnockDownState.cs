using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownState : State
{
    protected float knockDuration = 0.5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsKnockDown", true);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsKnockDown(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float dir = this.stateManager.CharacterCtrl.CharacterFlip.Direct * -1;
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(dir * 20f, -40f);
        if (!this.stateManager.CharacterCtrl.CharacterCollision.IsGround) return;
        this.stateManager.SetNextState(new BounceOnGroundState());
        // if (this.timed < this.knockDuration) return;
        // this.stateManager.SetNextStateToMain();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockDown", false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsKnockDown(false);
        AudioManager.Instance.AudioSource.PlayOneShot(
                   this.stateManager.CharacterCtrl.CharacterData.CharacterDataSO.groundHitClip);
    }
}
