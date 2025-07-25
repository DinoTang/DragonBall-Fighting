using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseKickState : BaseAttackState
{
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime > duration * 0.5f && InputManager.Instance.GetKickInput())
        {
            shouldCombo = true;
        }
    }
}
