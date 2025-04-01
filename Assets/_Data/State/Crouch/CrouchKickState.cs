using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchKickState : BaseCrouchHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 2;
        base.OnEnter(stateManager);
    }
}
