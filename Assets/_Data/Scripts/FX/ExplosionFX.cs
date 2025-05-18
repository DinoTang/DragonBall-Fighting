using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : PoolObj
{
    [SerializeField] protected string EFX_Name = "Default_Explosion";
    public override string GetName()
    {
        return this.EFX_Name;
    }
}
