using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeJumpFXDespawn : Despawn<SmokeJumpFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1;
    }
}
