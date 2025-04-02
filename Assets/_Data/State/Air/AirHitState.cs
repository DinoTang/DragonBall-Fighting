using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHitState : BaseAirHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.attackCounter = 1;
        base.OnEnter(stateManager);
    }
}
