using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillState : CharacterState
{
    protected SkillDataSO skill;
    protected float totalDuration;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsSkill", true);
        skill = stateManager.CharacterCtrl.CharacterSkillManager.CurrentSkill;
        this.totalDuration = this.GetTotalDuration();
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
        this.stateManager.CharacterCtrl.Animator.SetBool("IsSkill", false);
    }

    protected virtual float GetTotalDuration()
    {
        return this.skill.animationClip.length;
    }
}



