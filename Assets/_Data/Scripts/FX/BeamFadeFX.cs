using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamFadeFX : PoolObj
{
    [SerializeField] protected BeamSkillNameEnum beamSkillName;
    public override string GetName()
    {
        return this.beamSkillName.ToString();
    }

}
