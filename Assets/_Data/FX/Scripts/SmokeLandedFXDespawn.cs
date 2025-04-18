using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeLandedFXDespawn : Despawn<SmokeLandedFX>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 0.3f;
    }
}
