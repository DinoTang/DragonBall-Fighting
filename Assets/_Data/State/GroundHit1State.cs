using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit1State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        attackIndex = 1;
        this.stateManager.CharacterCtrl.Animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            if (shouldCombo)
            {
                this.stateManager.SetNextState(new GroundHit2State());
            }
            else
            {
                this.stateManager.SetNextStateToMain();
            }
        }
    }
}
