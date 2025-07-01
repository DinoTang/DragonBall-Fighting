using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : PoolObj
{
    [SerializeField] protected ExplosionFXEnum EFX_Name;
    public override string GetName()
    {
        return this.EFX_Name.ToString();
    }
}
