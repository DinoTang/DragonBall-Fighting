using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleFX : PoolObj
{
    [SerializeField] protected TeleFXNameEnum teleFXName;
    public override string GetName()
    {
        return this.teleFXName.ToString();
    }
}
