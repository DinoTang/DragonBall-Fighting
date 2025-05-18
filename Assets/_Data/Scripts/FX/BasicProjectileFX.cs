using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileFX : PoolObj
{
    [SerializeField] protected CharacterEnum characterEnum;
    public override string GetName()
    {
        return this.characterEnum.ToString();
    }

}
