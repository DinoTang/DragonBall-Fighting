using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit2State : BaseHitState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);

        //Attack
        attackIndex = 2;
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
                this.stateManager.SetNextState(new GroundHit3State());
            }
            else
            {
                this.stateManager.SetNextStateToMain();
            }
        }
    }
}
