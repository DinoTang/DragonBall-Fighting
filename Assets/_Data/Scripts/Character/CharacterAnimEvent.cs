using System.Collections;
using UnityEngine;

public class CharacterAnimEvent : CharacterAbstract
{
    private readonly float skillSpawnOffsetX = 2.5f;
    protected void EnableHitbox() => this.characterCtrl.CharacterDamageSender.EnableCollider();
    protected void DisableHitbox() => this.characterCtrl.CharacterDamageSender.DisableCollider();

    protected void EnableSpawnBasicProjectile()
    {
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        BasicProjSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), pos, this.characterCtrl);
    }

    protected void EnableSpawnBasicProjectileEffect()
    {
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        BasicProjFXSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), pos, this.characterCtrl);
    }

    protected void EnableSpawnSkillProj(SkillSlot slot)
    {
        SkillProjectile prefab = SkillProjSpawner.Instance.GetSkill(slot);
        if (prefab == null)
        {
            Debug.LogWarning($"{transform.parent.name}: Không tìm thấy SkillProjectile cho {slot}");
            return;
        }

        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        pos.x += this.characterCtrl.transform.localScale.x > 0 ? this.skillSpawnOffsetX : -this.skillSpawnOffsetX;

        SkillProjSpawner.Instance.Spawn(prefab, pos, this.characterCtrl);
    }

    protected void EnableSpawnBeamSkillProj(SkillSlot slot)
    {
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        pos.x += this.characterCtrl.transform.localScale.x > 0 ? this.skillSpawnOffsetX : -this.skillSpawnOffsetX;
        Vector2 dir = this.characterCtrl.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        SkillProjSpawner.Instance.StartBeamSkill(slot, pos, dir, this.characterCtrl);
    }

    protected void EnableSkillOne() => EnableSpawnSkillProj(SkillSlot.Skill_1);
    protected void EnableSkillTwo() => EnableSpawnBeamSkillProj(SkillSlot.Skill_2);
}
