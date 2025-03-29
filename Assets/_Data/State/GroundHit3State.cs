using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHit3State : BaseHitState
{
    public override void OnEnter()
    {
        base.OnEnter();

        //Attack
        attackIndex = 3;
        CharacterCtrl.Instance.Animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Fixedtime >= duration)
        {
            CharacterCtrl.Instance.StateManager.SetNextStateToMain();
        }
    }
}
