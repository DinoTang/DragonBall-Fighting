using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrongHitState : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 3;
        base.OnEnter(stateManager);
    }
}
