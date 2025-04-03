using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    protected float speed = 15f;
    protected Vector2 movement;
    protected float horizontal;

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (this.CanOtherAction()) return;

        this.UpdateMovement();
        this.ApplyMovement();
        this.SetMoveAnimation();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.ResetMoveAnimation();
    }
    protected void UpdateMovement()
    {
        this.horizontal = InputManager.Instance.GetHorizontal();

        if (this.horizontal != 0)
        {
            this.movement = new Vector2(this.horizontal, 0);
        }
        else
        {
            this.movement = Vector2.zero;
            this.ResetMoveAnimation();
            this.stateManager.SetNextState(new IdleCombatState());
        }
    }

    protected void ApplyMovement()
    {
        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        rb.velocity = new Vector2(this.movement.x * this.speed, rb.velocity.y);
    }

    protected void SetMoveAnimation()
    {
        bool isFacingRight = this.stateManager.transform.localScale.x == 1;

        ResetMoveAnimation();

        if (this.movement.x == 1)
        {
            this.animator.SetBool(isFacingRight ? "MoveForward" : "MoveBackward", true);
        }
        else if (this.movement.x == -1)
        {
            this.animator.SetBool(isFacingRight ? "MoveBackward" : "MoveForward", true);
        }
    }

    protected void ResetMoveAnimation()
    {
        this.animator.SetBool("MoveForward", false);
        this.animator.SetBool("MoveBackward", false);
    }

    protected bool CanOtherAction()
    {
        if (InputManager.Instance.GetJumpInput())
        {
            this.stateManager.SetNextState(new JumpState());
            return true;
        }
        if (InputManager.Instance.GetNormalHitInput())
        {
            this.stateManager.SetNextState(new GroundHit1State());
            return true;
        }
        if (InputManager.Instance.GetKickInput())
        {
            this.stateManager.SetNextState(new KickState());
            return true;
        }
        if (InputManager.Instance.GetStrongHitInput())
        {
            this.stateManager.SetNextState(new GroundStrongHitState());
            return true;
        }
        if (InputManager.Instance.GetBlockHitInput())
        {
            this.stateManager.SetNextState(new BlockState());
            return true;
        }
        if (InputManager.Instance.GetCrouchInput())
        {
            this.stateManager.SetNextState(new CrouchState());
            return true;
        }

        return false;
    }
}
