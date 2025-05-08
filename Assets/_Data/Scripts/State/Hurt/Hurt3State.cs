using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt3State : HurtState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.hurtCounter = 3;
        base.OnEnter(stateManager);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Fixedtime >= this.hurtDuration)
        {
            this.stateManager.SetNextState(new IdleCombatState());
        }
    }
}
