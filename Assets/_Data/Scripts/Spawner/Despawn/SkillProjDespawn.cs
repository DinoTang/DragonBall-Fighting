using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjDespawn : Despawn<SkillProjectile>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.LoadParent();
        this.LoadDurationTimeToDespawn();
    }

    protected void LoadDurationTimeToDespawn()
    {
        BeamSkillProjectile beamSkillProjectile = this.parent as BeamSkillProjectile;
        if (beamSkillProjectile == null) return;
        this.lifeTime = beamSkillProjectile.SKillDuration;
    }
}
