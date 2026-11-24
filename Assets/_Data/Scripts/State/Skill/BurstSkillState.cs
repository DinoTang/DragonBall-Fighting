using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BurstSkillState : EnergySkillState
{
    public override void OnUpdate()
    {
        base.OnUpdate();

        float distance = this.stateManager.CharacterCtrl.CharacterFlip.DistanceOther;
        if (distance > 3) return;
        Vector2 pos = this.stateManager.CharacterCtrl.Opponent.transform.position;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Burst_Explosion.ToString(), pos, this.stateManager.CharacterCtrl);
        this.stateManager.CharacterCtrl.Opponent.StateManager.SetNextState(new KnockBackState());
        SkillProjSpawner.Instance.Spawn(SkillNameEnum.Skill_Burst.ToString(), pos, this.stateManager.CharacterCtrl);
    }
}

