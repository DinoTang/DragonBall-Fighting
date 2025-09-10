using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Base class
public class EnergyProjectileSkillState : SkillState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        stateManager.CharacterCtrl.CharacterAnimEvent.StopCharacterActions();
        AudioManager.Instance.AudioSource.PlayOneShot(this.skill.sfx);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > this.totalDuration) this.stateManager.SetNextStateToMain();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.CharacterAnimEvent.EnableCharacterActions();
    }


}



