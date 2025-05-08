using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo1 : BaseCombo
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetInteger("ComboCount", 1);
    }
}
