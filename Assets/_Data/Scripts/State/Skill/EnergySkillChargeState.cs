using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Base class
public class EnergySkillChargeState : SkillState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        SkillChargeSO skillChargeSO = this.skill as SkillChargeSO;
        AudioManager.Instance.AudioSource.PlayOneShot(skillChargeSO.SFXCharge);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > this.totalDuration) this.stateManager.SetNextStateToMain();
    }

}



