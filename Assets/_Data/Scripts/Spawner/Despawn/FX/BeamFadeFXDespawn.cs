using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamFadeFXDespawn : Despawn<BeamFadeFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 3;
    }
}
