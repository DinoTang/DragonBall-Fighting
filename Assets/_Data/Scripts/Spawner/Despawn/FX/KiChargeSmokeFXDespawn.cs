using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiChargeSmokeFXDespawn : Despawn<KiChargeSmokeFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1;
    }
}
