using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    protected float speed = 15f;
    protected float jumpForce = 50f;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsJump", true);
        this.stateManager.CharacterCtrl.Rgb.velocity =
            new Vector2(this.stateManager.CharacterCtrl.Rgb.velocity.x, this.jumpForce);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.MoveOnAir();
        if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new AirStrongHitState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsJump", false);
    }

    protected void MoveOnAir()
    {
        float xInput = InputManager.Instance.GetHorizontal();
        if (xInput != 0)
        {
            this.stateManager.CharacterCtrl.Rgb.velocity =
            new Vector2(xInput * this.speed, this.stateManager.CharacterCtrl.Rgb.velocity.y);
        }
    }
}
