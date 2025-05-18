using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjFXDespawn : Despawn<BasicProjectileFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1;
    }
}
