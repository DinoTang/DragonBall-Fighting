using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaMoveState : MoveState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsMove", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.Flipping();
    }
    public override void OnExit()
    {
        base.OnExit();
        // this.ResetMoveAnimation();
        this.animator.SetBool("IsMove", false);
    }
    protected void Flipping()
    {
        if (this.horizontal > 0) this.stateManager.transform.localScale = new Vector3(1, 1, 1);
        else if (this.horizontal < 0) this.stateManager.transform.localScale = new Vector3(-1, 1, 1);
    }
    protected override void ApplyMovement()
    {
        Rigidbody2D rb = this.stateManager.CharacterCtrl.Rgb;
        rb.velocity = new Vector2(this.movement.x * this.speed, rb.velocity.y);
    }
    protected override void SetMoveAnimation()
    {

    }

    protected override void ResetMoveAnimation()
    {

    }

    protected override bool CanOtherAction()
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
