using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsBlock", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseBlockHitInput()) this.stateManager.SetNextState(new IdleCombatState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsBlock", false);
    }
}
