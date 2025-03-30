using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsMoving", true);
    }

    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsMoving", false);
    }
}
