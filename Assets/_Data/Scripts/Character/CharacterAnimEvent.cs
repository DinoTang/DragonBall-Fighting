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

    protected void EnableSpawnBasicProjectile()
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        BasicProjSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), newPos, this.characterCtrl);
    }

    protected void EnableSpawnBasicProjectileEffect()
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        BasicProjFXSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), newPos, this.characterCtrl);
    }

    protected void EnableSpawnSkillProjectileOne()
    {
        string skillProj_Name = SkillProjSpawner.Instance.GetSkill(SkillSlot.Skill_1);
        if (skillProj_Name == "")
        {
            Debug.Log(transform.parent.name + ": Khong tim thay ten cua SkillProjectile");
            return;
        }
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        newPos.x += 1.5f;
        SkillProjSpawner.Instance.Spawn(skillProj_Name, newPos, this.characterCtrl);
    }
}
