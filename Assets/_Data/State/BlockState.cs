using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.HoldBlockHitInput()) this.stateManager.SetNextState(new IdleCombatState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsBlock", false);
    }
}
