using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Base class
public class SkillState : State
{
    protected SkillDataSO skill;
    protected float totalDuration;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        skill = stateManager.CharacterCtrl.CharacterSkillManager.CurrentSkill;
        totalDuration = this.GetTotalDuration();
        stateManager.CharacterCtrl.CharacterAnimEvent.StopCharacterActions();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed > totalDuration) this.stateManager.SetNextStateToMain();
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

    protected virtual float GetTotalDuration()
    {
        return this.skill.animationClip.length;
    }
}



