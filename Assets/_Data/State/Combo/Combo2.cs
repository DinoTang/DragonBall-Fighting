using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo2 : BaseCombo
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetInteger("ComboCount", 2);
    }
}
