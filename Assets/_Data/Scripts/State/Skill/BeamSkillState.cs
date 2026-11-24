using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeamSkillState : EnergySkillChargeState
{
    BeamSkillDataSO beamSkillDataSO;
    protected override float GetTotalDuration()
    {
        this.beamSkillDataSO = this.skill as BeamSkillDataSO;
        if (beamSkillDataSO == null) return 0;
        return base.GetTotalDuration() + beamSkillDataSO.duration + beamSkillDataSO.endBeamSkillAnim.length;
    }
}

