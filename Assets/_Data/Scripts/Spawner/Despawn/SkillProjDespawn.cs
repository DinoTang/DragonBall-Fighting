using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjDespawn : Despawn<SkillProjectile>
{
    protected override void ResetValue()
    {
        this.LoadParent();
        base.ResetValue();
        this.LoadDurationTimeToDespawn();
    }
    protected void LoadDurationTimeToDespawn()
    {
        BeamSkillProjectile beamSkillProjectile = this.parent as BeamSkillProjectile;
        if (beamSkillProjectile == null)
        {
            Debug.LogWarning("deo co");
            return;
        }
        this.lifeTime = beamSkillProjectile.BeamDuration;
    }
}
