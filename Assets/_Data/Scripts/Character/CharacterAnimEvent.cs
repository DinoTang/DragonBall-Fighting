using System.Collections;
using UnityEngine;

public class CharacterAnimEvent : CharacterAbstract
{
    private readonly float skillSpawnOffsetX = 3.5f;
    protected void EnableHitbox() => this.characterCtrl.CharacterDamageSender.EnableCollider();
    protected void DisableHitbox() => this.characterCtrl.CharacterDamageSender.DisableCollider();
    public void StopCharacterActions()
    {
        this.characterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void EnableCharacterActions()
    {
        this.characterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.characterCtrl.Rgb.WakeUp();
    }
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
        pos.x += this.characterCtrl.CharacterFlip.IsFacingRight ? this.skillSpawnOffsetX : -this.skillSpawnOffsetX;

        SkillProjSpawner.Instance.Spawn(prefab, pos, this.characterCtrl);
    }

    protected void EnableSpawnBeamSkillProj(SkillSlot slot)
    {
        Vector2 pos = this.characterCtrl.FirePoint.transform.position;
        pos.x += this.characterCtrl.CharacterFlip.IsFacingRight ? this.skillSpawnOffsetX : -this.skillSpawnOffsetX;
        Vector2 dir = this.characterCtrl.CharacterFlip.IsFacingRight ? Vector2.right : Vector2.left;

        SkillProjSpawner.Instance.StartBeamSkill(slot, pos, dir, this.characterCtrl);
    }

    protected void EnableSkillOne() => EnableSpawnSkillProj(SkillSlot.Skill_1);
    protected void EnableSkillTwo() => EnableSpawnBeamSkillProj(SkillSlot.Skill_2);
}
