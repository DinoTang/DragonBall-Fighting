using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallKnockBackState : CharacterState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsFallKnockBack", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!this.stateManager.CharacterCtrl.CharacterCollision.IsGround) return;
        this.stateManager.SetNextState(new BounceOnGroundState());
    }

    public override void OnExit()
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsFallKnockBack", false);
    }
}
