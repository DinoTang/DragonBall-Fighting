using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.CharacterAnimEvent.StopCharacterActions();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Timed > 7.2f) this.stateManager.SetNextStateToMain();
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.CharacterCombo.ClearCurrentSkill();
        this.stateManager.CharacterCtrl.CharacterAnimEvent.EnableCharacterActions();
    }
}
