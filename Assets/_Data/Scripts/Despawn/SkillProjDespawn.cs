using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjDespawn : Despawn<SkillProjectile>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 5f;
    }
}
