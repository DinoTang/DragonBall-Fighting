using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHurtState : HurtState
{
    protected float hurtDuration = 1f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);

        this.duration = 0.2f;
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, 1f);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 0;
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 15;
    }
}
