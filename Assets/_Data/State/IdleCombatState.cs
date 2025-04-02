using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCombatState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsHardMode", true);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.stateManager.CharacterCtrl.Rgb.velocity.y);
    }
    public override void OnUpdate()
    {
        if (InputManager.Instance.GetHorizontal() != 0) this.stateManager.SetNextState(new MovingState());
        if (InputManager.Instance.GetNormalHitInput()) this.stateManager.SetNextState(new GroundHit1State());
        if (InputManager.Instance.GetJumpInput()) this.stateManager.SetNextState(new JumpState());
        if (InputManager.Instance.GetCrouchInput()) this.stateManager.SetNextState(new CrouchState());
        if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new GroundStrongHitState());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new KickState());
        if (InputManager.Instance.GetChargeInput()) this.stateManager.SetNextState(new ChargeState());
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new BlockState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsHardMode", false);
    }
}
