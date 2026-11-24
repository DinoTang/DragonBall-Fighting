using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele1 : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("Tele1", true);

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= 0.1f)
        {
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("Tele1", false);
    }
}
