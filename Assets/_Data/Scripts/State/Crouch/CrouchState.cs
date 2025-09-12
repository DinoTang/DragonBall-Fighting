using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsCrouch", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseCrouchInput()) this.stateManager.SetNextState(new IdleState());
        if (InputManager.Instance.GetHitInput(this.playerType)) this.stateManager.SetNextState(new CrouchHitState());
        // if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new CrouchStrongHitState());
        if (InputManager.Instance.GetKickInput(this.playerType)) this.stateManager.SetNextState(new CrouchKickState());
        this.CrouchBlockHit();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsCrouch", false);
    }
    protected void CrouchBlockHit()
    {
        if (InputManager.Instance.GetBlockInput(this.playerType)) this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", true);
        if (InputManager.Instance.ReleaseBlockHitInput(this.playerType)) this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", false);
    }
}
