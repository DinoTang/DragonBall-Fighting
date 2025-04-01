using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchHitState : BaseCrouchHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 1;
        base.OnEnter(stateManager);
    }
}
