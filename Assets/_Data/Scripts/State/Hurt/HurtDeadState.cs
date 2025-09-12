using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtDeadState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsHurt", true);
        this.animator.SetInteger("HurtCounter", 3);
        CameraManager.Instance.Shake(0.05f, 0.05f);
        this.stateManager.CharacterCtrl.CharacterCollision.IsGround = false;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > this.duration - 0.2f) this.stateManager.SetNextStateToMain();

        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(this.stateManager.transform.localScale.x * -10f, 15f);
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
    }
}
