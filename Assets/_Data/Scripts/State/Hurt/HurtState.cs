using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsHurt", true);
        int indexRand = Random.Range(0, 3);
        this.animator.SetInteger("HurtCounter", indexRand);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= this.duration)
        {
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
    }
}
