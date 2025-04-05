using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt2State : HurtState
{
    public override void OnEnter(StateManager stateManager)
    {
        this.hurtCounter = 2;
        base.OnEnter(stateManager);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Fixedtime >= this.hurtDuration)
        {
            if (shouldHurt)
            {
                this.stateManager.SetNextState(new Hurt3State());
            }
            else
            {
                this.stateManager.SetNextState(new IdleCombatState());
            }
        }
    }
}
