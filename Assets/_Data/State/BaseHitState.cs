using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitState : State
{
    public float duration = 0.3f;
    protected bool shouldCombo = false;
    protected int attackIndex;
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!this.stateManager.CharacterCtrl.CharacterIntro.IsReady) return;
        if (Fixedtime >= duration * 0.5f && InputManager.Instance.GetAttackHitInput())
        {
            shouldCombo = true;
        }
    }
}
