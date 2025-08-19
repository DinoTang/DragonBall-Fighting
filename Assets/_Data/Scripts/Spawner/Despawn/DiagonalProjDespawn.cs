using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalProjDespawn : Despawn<DiagonalProjectile>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 4;
    }
}
