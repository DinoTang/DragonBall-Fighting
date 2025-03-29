using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit1State : BaseHitState
{
    public override void OnEnter()
    {
        base.OnEnter();
        //Attack
        attackIndex = 1;
        CharacterCtrl.Instance.Animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            if (shouldCombo)
            {
                CharacterCtrl.Instance.StateManager.SetNextState(new GroundHit2State());
            }
            else
            {
                CharacterCtrl.Instance.StateManager.SetNextStateToMain();
            }
        }
    }
}
