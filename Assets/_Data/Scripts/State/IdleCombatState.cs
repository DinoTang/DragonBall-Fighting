using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCombatState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsIdle", true);
        this.stateManager.CharacterCtrl.Rgb.velocity = new Vector2(0, this.stateManager.CharacterCtrl.Rgb.velocity.y);
    }
    public override void OnUpdate()
    {
        if (this.stateManager.IsClone) return;
        this.stateManager.CharacterCtrl.CharacterFlip.Flipping();
        if (InputManager.Instance.GetHorizontal() != 0) this.stateManager.SetNextState(new MoveState());
        if (InputManager.Instance.GetUpInput()) this.stateManager.SetNextState(new JumpState());
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new BlockState());
        if (InputManager.Instance.GetHitInput()) this.stateManager.SetNextState(new GroundHit1State());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new KickState());
        if (InputManager.Instance.GetChargeInput()) this.stateManager.SetNextState(new ChargeState());
        if (InputManager.Instance.GetLauncherInput()) this.stateManager.SetNextState(new LauncherState());
        if (InputManager.Instance.GetPowerFiredInput()) this.stateManager.SetNextState(new PowerFiredState());

    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsIdle", false);
    }
}
