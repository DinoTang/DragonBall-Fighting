using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyKiChargeFXDespawn : Despawn<ReadyKiChargeFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1;
    }
}
