using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeamSkillState : EnergyProjectileSkillState
{
    protected override float GetTotalDuration()
    {
        BeamSkillDataSO beamSkillDataSO = this.skill as BeamSkillDataSO;
        if (beamSkillDataSO == null) return 0;
        return base.GetTotalDuration() + beamSkillDataSO.duration + beamSkillDataSO.endBeamSkillAnim.length;
    }
}

