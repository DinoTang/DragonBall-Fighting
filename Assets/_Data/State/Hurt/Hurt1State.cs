using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt1State : HurtState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.hurtCounter = 1;
        base.OnEnter(stateManager);
    }
}
