using System.Collections;
using UnityEngine;

public class CharacterAnimEvent : CharacterAbstract
{
    private readonly float skillSpawnOffsetX = 3.5f;
    public void EnableHitbox() => this.characterCtrl.CharacterDamageSender.EnableCollider();
    public void DisableHitbox() => this.characterCtrl.CharacterDamageSender.DisableCollider();
    protected void EnableKnockUp() => this.characterCtrl.CharacterDamageSender.SetIsKnockingUp(true);
    protected void DisablenockUp() => this.characterCtrl.CharacterDamageSender.SetIsKnockingUp(false);
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
        BasicProjSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), this.GetPosToSpawn(), this.characterCtrl);
    }
    protected void EnableSpawnDiagonalProjectile()
    {
        DiagonalProjectile diagonalProjectile = DiagonalProjSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), this.GetPosToSpawn(), this.characterCtrl);
    }
    protected void EnableSpawnBasicProjectileEffect()
    {
        BasicProjFXSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), this.GetPosToSpawn(), this.characterCtrl);
    }

    protected void EnableSpawnSkillProj(SkillSlot slot)
    {
        SkillProjectile prefab = SkillProjSpawner.Instance.GetSkill(slot);
        if (prefab == null)
        {
            Debug.LogWarning($"{transform.parent.name}: Không tìm thấy SkillProjectile cho {slot}");
            return;
        }

        SkillProjSpawner.Instance.Spawn(prefab, this.GetPosToSpawn(this.skillSpawnOffsetX), this.characterCtrl);
    }

    protected void EnableSpawnBeamSkillProj(SkillSlot slot)
    {
        Vector2 pos = this.GetPosToSpawn(this.skillSpawnOffsetX);
        Vector2 dir = this.characterCtrl.CharacterFlip.IsFacingRight ? Vector2.right : Vector2.left;

        SkillProjSpawner.Instance.StartBeamSkill(slot, pos, dir, this.characterCtrl);
    }

    protected void EnableSkillOne() => EnableSpawnSkillProj(SkillSlot.Skill_1);
    protected void EnableSkillTwo() => EnableSpawnBeamSkillProj(SkillSlot.Skill_2);

    protected Vector2 GetPosToSpawn(float posX = 0, float posY = 0)
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        newPos.x += this.characterCtrl.CharacterFlip.Direct * posX;
        newPos.y += this.characterCtrl.CharacterFlip.Direct * posY;
        return newPos;
    }
}
