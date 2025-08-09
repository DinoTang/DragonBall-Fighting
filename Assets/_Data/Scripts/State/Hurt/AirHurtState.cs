using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHurtState : HurtState
{
    protected float hurtDuration = 1f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.SetUpIsHitPos();
        this.duration = 0.2f;
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 0;
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, 1f);
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 15;
    }

    protected void SetUpIsHitPos()
    {
        Vector2 isHitPos = this.stateManager.CharacterCtrl.Opponent.transform.position;
        isHitPos.x -= this.stateManager.CharacterCtrl.CharacterFlip.Direct * 1.5f;
        this.stateManager.transform.position = isHitPos;
    }
}
