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
        float skillDuration = this.stateManager.CharacterCtrl.CharacterSkillManager.CurrentSkill.duration;
        float animationClipDuration = this.stateManager.CharacterCtrl.CharacterSkillManager.CurrentSkill.animationClip.length;
        float totalDuration = skillDuration + animationClipDuration;
        if (this.Timed > totalDuration) this.stateManager.SetNextStateToMain();
    }
    public override void OnExit()
    {
        base.OnExit();
        var skillManager = this.stateManager.CharacterCtrl.CharacterSkillManager;
        if (skillManager.CurrentSkill != null)
        {
            skillManager.StartCooldownFor(skillManager.CurrentSkill.skillName);
            skillManager.CurrentSkill = null;
        }

        this.stateManager.CharacterCtrl.CharacterCombo.ClearCurrentSkill();
        this.stateManager.CharacterCtrl.CharacterAnimEvent.EnableCharacterActions();
    }

}
