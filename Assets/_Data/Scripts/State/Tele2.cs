using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele2 : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("Tele2", true);
        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Fixedtime >= 0.1f)
        {
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("Tele2", false);
    }
}
