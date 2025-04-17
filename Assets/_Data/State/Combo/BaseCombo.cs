using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombo : State
{

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.duration = 1f;
        this.animator.SetBool("IsCombo", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Fixedtime >= this.duration) this.stateManager.SetNextStateToMain();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetInteger("ComboCount", 0);
        this.animator.SetBool("IsCombo", false);
    }
}
