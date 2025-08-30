using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsInOutro", true);
        this.animator.SetInteger("PoseCounter", 1);
    }
}
