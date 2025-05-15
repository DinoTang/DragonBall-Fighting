using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaShotSkillDespawn : Despawn<VegetaShotSkill>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 5f;
    }
}
