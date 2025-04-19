using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected float speed = 15f;
    protected Vector2 movement;
    protected float horizontal;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsMove", true);
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
        this.horizontal = InputManager.Instance.GetHorizontal();

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
        if (TrySwitchState(new JumpState(), InputManager.Instance.GetUpInput())) return true;
        if (TrySwitchState(new GroundHit1State(), InputManager.Instance.GetHitInput())) return true;
        if (TrySwitchState(new KickState(), InputManager.Instance.GetKickInput())) return true;
        if (TrySwitchState(new GroundStrongHitState(), InputManager.Instance.GetStrongHitInput())) return true;
        if (TrySwitchState(new BlockState(), InputManager.Instance.GetBlockHitInput())) return true;
        return false;
    }

    protected bool TrySwitchState(State newState, bool input)
    {
        if (input)
        {
            stateManager.StopAddVelocity();
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
        if ((this.movement.x < 0 && stateManager.transform.localScale.x == 1) || (this.movement.x > 0 && stateManager.transform.localScale.x == -1))
        {
            this.animator.SetBool("IsForwardMove", false);
            this.animator.SetBool("IsBackMove", true);
        }
        else if (this.movement.x > 0 && stateManager.transform.localScale.x == 1 || (this.movement.x < 0 && stateManager.transform.localScale.x == -1))
        {
            this.animator.SetBool("IsForwardMove", true);
            this.animator.SetBool("IsBackMove", false);
        }
    }
}
