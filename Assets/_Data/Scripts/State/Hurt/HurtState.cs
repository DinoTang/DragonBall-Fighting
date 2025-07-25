using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected float hurtDuration = 0.5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsHurt", true);

        this.stateManager.CharacterCtrl.Rgb.velocity = Vector2.zero;
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 0;

        int indexRand = Random.Range(0, 3);
        this.animator.SetInteger("HurtCounter", indexRand);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= hurtDuration)
        {
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 15;
    }
}
