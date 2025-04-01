using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsCrouch", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.HoldCrouchInput()) this.stateManager.SetNextState(new IdleCombatState());
        if (InputManager.Instance.GetNormalHitInput()) this.stateManager.SetNextState(new CrouchHitState());
        if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new CrouchStrongHitState());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new CrouchKickState());
        this.CrouchBlockHit();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsCrouch", false);
    }
    protected void CrouchBlockHit()
    {
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", true);
        if (InputManager.Instance.HoldBlockHitInput()) this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", false);
    }
}
