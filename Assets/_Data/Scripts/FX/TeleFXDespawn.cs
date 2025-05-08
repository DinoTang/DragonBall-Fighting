using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleFXDespawn : Despawn<TeleFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1f;
    }
}
