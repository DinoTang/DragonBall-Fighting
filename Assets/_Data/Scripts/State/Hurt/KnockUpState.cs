using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUpState : State
{
    protected float knockDuration = 0.5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsKnockUp", true);
        this.stateManager.CharacterCtrl.IsGround = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.timed >= this.knockDuration)
        {
            float dir = this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight ? -1 : 1;
            this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(dir * 10f, 30f);
            this.stateManager.SetNextState(new AirState());
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockUp", false);
    }
}
