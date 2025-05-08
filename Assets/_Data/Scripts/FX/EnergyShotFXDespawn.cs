using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotFXDespawn : Despawn<EnergyShotFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 1;
    }
}
