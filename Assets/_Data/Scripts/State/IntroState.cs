using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsInOutro", true);
        this.animator.SetInteger("PoseCounter", 0);
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsInOutro", false);
        this.animator.SetInteger("PoseCounter", -1);
    }
}
