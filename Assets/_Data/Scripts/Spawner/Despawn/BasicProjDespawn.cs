using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjDespawn : Despawn<BasicProjectile>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 4;
    }
}
