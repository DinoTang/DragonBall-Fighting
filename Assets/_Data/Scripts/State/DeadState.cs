using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsDead", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        
    }

}
