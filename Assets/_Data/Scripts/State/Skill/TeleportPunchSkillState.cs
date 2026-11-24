using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeleportPunchSkillState : SkillState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        var teleportSkill = this.skill as TeleportPunchSkillSO;
        teleportSkill.Execute(this.stateManager.CharacterCtrl, this.stateManager.CharacterCtrl.Opponent);
    }

    protected override float GetTotalDuration()
    {
        TeleportPunchSkillSO tpPunchSkillSO = this.skill as TeleportPunchSkillSO;
        if (tpPunchSkillSO == null) return 0;
        return tpPunchSkillSO.firstHitAnim.length + tpPunchSkillSO.secondHitAnim.length + tpPunchSkillSO.waitBetween;
    }
}


