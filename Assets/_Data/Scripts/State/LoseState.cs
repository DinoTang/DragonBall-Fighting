using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsInOutro", true);
        this.animator.SetInteger("PoseCounter", 2);
    }
}
