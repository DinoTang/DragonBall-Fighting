using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : CharacterState
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
