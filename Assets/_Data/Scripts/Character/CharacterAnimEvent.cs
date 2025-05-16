using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterAnimEvent : CharacterAbstract
{
    protected void EnableHitbox()
    {
        this.characterCtrl.CharacterDamageSender.EnableCollider();
    }

    protected void DisableHitbox()
    {
        this.characterCtrl.CharacterDamageSender.DisableCollider();
    }

    protected void EnableSpawnEnergyShot()
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        EnergyShot energyShot = EnergyShotSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), newPos);
        energyShot.Init(this.characterCtrl);
    }

    protected void EnableSpawnShotEffect()
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        EnergyShotFX energyShotFX = EnergyShotFXSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), newPos);
        energyShotFX.Init(this.characterCtrl);
    }

    protected void EnableSpawnEnergyShotSkill_1()
    {
        string ShotSkill_Name = ShotSkillSpawner.Instance.GetSkill(SkillSlot.Skill_1);
        if (ShotSkill_Name == "")
        {
            Debug.Log(transform.parent.name + ": Khong tim thay ten cua ShotSKill");
            return;
        }
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        newPos.x += 1.5f;
        ShotSkill vegetaSkill = ShotSkillSpawner.Instance.Spawn(ShotSkill_Name, newPos);
        vegetaSkill.Init(this.characterCtrl);
    }
}
