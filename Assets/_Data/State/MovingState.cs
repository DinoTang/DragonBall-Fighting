using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    protected float speed = 15f;
    private Vector2 movement;
    private float horizontal;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsMoving", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (InputManager.Instance.GetJumpInput())
        {
            this.stateManager.SetNextState(new JumpState());
            return;
        }
        if (InputManager.Instance.GetNormalHitInput())
        {
            this.stateManager.SetNextState(new GroundHit1State());
            return;
        }
        if (InputManager.Instance.GetKickInput())
        {
            this.stateManager.SetNextState(new KickState());
            return;
        }
        if (InputManager.Instance.GetStrongHitInput())
        {
            this.stateManager.SetNextState(new GroundStrongHitState());
            return;
        }
        if (InputManager.Instance.GetBlockHitInput())
        {
            this.stateManager.SetNextState(new BlockState());
            return;
        }
        if (InputManager.Instance.GetCrouchInput())
        {
            this.stateManager.SetNextState(new CrouchState());
            return;
        }
        this.UpdateMovement();
        this.ApplyMovement();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsMoving", false);
    }

    private void UpdateMovement()
    {
        this.horizontal = InputManager.Instance.GetHorizontal();

        if (this.horizontal != 0)
        {
            this.Flipping();
            this.movement = new Vector2(this.horizontal, 0);
        }
        else
        {
            this.movement = Vector2.zero;
            this.stateManager.SetNextState(new IdleCombatState());
        }
    }

    private void ApplyMovement()
    {
        this.stateManager.CharacterCtrl.Rgb.velocity =
            new Vector2(this.movement.x * this.speed, this.stateManager.CharacterCtrl.Rgb.velocity.y);
    }

    private void Flipping()
    {
        if (this.horizontal > 0)
        {
            this.stateManager.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.stateManager.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
