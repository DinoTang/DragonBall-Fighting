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
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float dir = this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight ? -1 : 1;
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(dir * 10f, -40f);
        if (!this.stateManager.CharacterCtrl.CharacterGroundCollision.IsGround) return;
        this.stateManager.SetNextState(new BounceOnGroundState());
        // if (this.timed < this.knockDuration) return;
        // this.stateManager.SetNextStateToMain();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockDown", false);
    }
}
