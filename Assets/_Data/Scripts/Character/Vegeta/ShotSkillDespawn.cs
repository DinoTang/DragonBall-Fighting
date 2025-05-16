using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSkillDespawn : Despawn<ShotSkill>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 5f;
    }
}
