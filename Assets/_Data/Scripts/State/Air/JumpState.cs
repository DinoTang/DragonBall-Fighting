using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    protected float jumpForce = 40f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.IsGround = false;
        this.animator.SetBool("IsJump", true);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.jumpForce);
        SmokeJumpFXSpawner.Instance.Spawn(SmokeJumpFXSpawner.Instance.nameFX, this.animator.transform.position);

        this.stateManager.SetNextState(new AirState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsJump", false);
    }
}
