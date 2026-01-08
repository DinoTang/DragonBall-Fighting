using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CharacterState
{
    protected float speed = 15f;
    protected Vector2 movement;
    protected int horizontal;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsMove", true);
        AudioManager.Instance.PlayCharacterSound(CharacterSoundType.Move);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (HandleOtherActions()) return;
        this.UpdateMovement();
        this.ApplyMovement();
        this.SetAnimMove();
        // this.Flipping();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsMove", false);
        this.ResetAnimMove();
    }
    protected virtual void UpdateMovement()
    {
        this.horizontal = InputManager.Instance.GetMoveInput(this.playerType);

        if (this.horizontal != 0)
        {
            this.movement = new Vector2(this.horizontal, 0);
        }
        else
        {
            this.movement = Vector2.zero;
            this.stateManager.SetNextStateToMain();
        }
    }
    public void StopAddVelocity()
    {
        this.stateManager.CharacterCtrl.Rgb.velocity = Vector2.zero;
    }
    protected virtual void ApplyMovement()
    {
        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        rb.velocity = new Vector2(this.movement.x * this.speed, rb.velocity.y);
    }
    // protected void Flipping()
    // {
    //     if (this.horizontal > 0) this.stateManager.transform.localScale = new Vector3(1, 1, 1);
    //     else if (this.horizontal < 0) this.stateManager.transform.localScale = new Vector3(-1, 1, 1);
    // }
    protected virtual bool HandleOtherActions()
    {
        if (TrySwitchState(new JumpState(), InputManager.Instance.GetJumpInput(this.playerType))) return true;
        if (TrySwitchState(new GroundHit1State(), InputManager.Instance.GetHitInput(this.playerType))) return true;
        if (TrySwitchState(new GroundKick1State(), InputManager.Instance.GetKickInput(this.playerType))) return true;
        if (TrySwitchState(new BlockState(), InputManager.Instance.GetBlockInput(this.playerType))) return true;
        // if (TrySwitchState(new SuperDashState(), InputManager.Instance.GetSuperDashInput())) return true;
        return false;
    }

    protected bool TrySwitchState(CharacterState newState, bool input)
    {
        if (input)
        {
            this.StopAddVelocity();
            stateManager.SetNextState(newState);
            return true;
        }
        return false;
    }
    protected void ResetAnimMove()
    {
        this.animator.SetBool("IsForwardMove", false);
        this.animator.SetBool("IsBackMove", false);
    }
    protected void SetAnimMove()
    {
        if (this.movement.x == 0)
        {
            this.ResetAnimMove();
        }
        if ((this.movement.x < 0 && this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight)
            || (this.movement.x > 0 && !this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight))
        {
            this.animator.SetBool("IsForwardMove", false);
            this.animator.SetBool("IsBackMove", true);
        }
        else if (this.movement.x > 0 && this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight
                || (this.movement.x < 0 && !this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight))
        {
            this.animator.SetBool("IsForwardMove", true);
            this.animator.SetBool("IsBackMove", false);
        }
    }
}
