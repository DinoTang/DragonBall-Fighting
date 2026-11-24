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
        if (skillChargeSO != null)
        {
            if (skillChargeSO.SFXCharge != null)
                AudioManager.Instance.AudioSource.PlayOneShot(skillChargeSO.SFXCharge);
            if (skillChargeSO.HasSuperFreeze)
            {
                SuperFreezeManager.Instance?.TriggerSuperFreeze(this.stateManager.CharacterCtrl);
            }
        }
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > this.totalDuration) this.stateManager.SetNextStateToMain();
    }

    public override void OnExit()
    {
        base.OnExit();
        SuperFreezeManager.Instance?.EndSuperFreezeIfCaster(this.stateManager.CharacterCtrl);
    }

}



