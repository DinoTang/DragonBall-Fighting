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
        if (Input.GetKeyUp(KeyCode.S)) this.stateManager.SetNextState(new IdleCombatState());
        if (InputManager.Instance.GetNormalHitInput()) this.CrouchHit();
        if (InputManager.Instance.GetStrongHitInput()) this.CrouchStrongHit();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsCrouch", false);
    }

    protected void CrouchHit()
    {
        this.stateManager.CharacterCtrl.Animator.SetTrigger("CrouchHit");
    }
    protected void CrouchStrongHit()
    {
        this.stateManager.CharacterCtrl.Animator.SetTrigger("CrouchStrongHit");
    }
}
