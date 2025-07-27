using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockdownIdleState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.duration = 1.5f;
        this.animator.SetBool("IsKnockDownIdle", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= this.duration)
        {
            this.stateManager.SetNextState(new Tele2());
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsKnockDownIdle", false);
    }


}
