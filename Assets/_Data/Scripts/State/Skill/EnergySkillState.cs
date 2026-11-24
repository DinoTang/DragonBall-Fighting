using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Base class
public class EnergySkillState : SkillState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        // AudioManager.Instance.AudioSource.PlayOneShot(this.skill.sfx);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > this.totalDuration) this.stateManager.SetNextStateToMain();
    }
}



