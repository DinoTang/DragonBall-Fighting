using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFXDespawn : Despawn<ExplosionFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 3;
    }
}
