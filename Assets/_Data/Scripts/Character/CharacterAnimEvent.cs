using System.Collections;
using UnityEngine;

public class CharacterAnimEvent : CharacterAbstract
{
    private readonly float skillSpawnOffsetX = 3.5f;
    public void EnableHitbox() => this.characterCtrl.CharacterDamageSender.EnableCollider();
    public void DisableHitbox() => this.characterCtrl.CharacterDamageSender.DisableCollider();
    protected void EnableKnockUp() => this.characterCtrl.CharacterDamageSender.SetIsKnockingUp(true);
    // Kept for backward compatibility with existing Unity .anim events
    protected void DisablenockUp() => DisableKnockUp();
    public void DisableKnockUp() => this.characterCtrl.CharacterDamageSender.SetIsKnockingUp(false);
    public void EnableZoomCamera() => CameraManager.Instance.Focus(this.transform.position, 2.5f);
    public void StopCharacterActions()
    {
        this.characterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void EnableCharacterActions()
    {
        this.characterCtrl.Rgb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.characterCtrl.Rgb.WakeUp();
    }

    protected void EnableSpawnBasicProjectileEffect()
    {
        string name = this.characterCtrl.CharacterDataSO.characterName.ToString();
        BasicProjFXSpawner.Instance.Spawn(name, this.GetPosToSpawn(), this.characterCtrl);
    }

    protected SkillProjectile EnableSpawnSkillProj(SkillNameEnum skillName)
    {
        SkillProjectile skill = SkillProjSpawner.Instance.GetSkill(skillName);
        if (skill == null)
        {
            Debug.LogWarning($"{transform.parent.name}: Không tìm thấy SkillProjectile cho {skillName}");
            return null;
        }
        skill = SkillProjSpawner.Instance.Spawn(skill, this.GetPosToSpawn(this.skillSpawnOffsetX), this.characterCtrl);
        skill.SkillProjCtrl.Owner = this.characterCtrl;
        return skill;
    }

    protected void EnableSpawnStraightBeamSkillProj(SkillNameEnum skillName, float offsetX = 0, float offsetY = 0)
    {
        Vector2 pos = this.GetPosToSpawn(offsetX, offsetY);
        Vector2 dir = this.characterCtrl.CharacterFlip.IsFacingRight ? Vector2.right : Vector2.left;

        SkillProjSpawner.Instance.StartStraightBeamSkill(skillName, pos, dir, this.characterCtrl);
    }
    protected void EnableSpawnDiagonalBeamSkillProj(SkillNameEnum skillName)
    {
        Vector2 dir = this.characterCtrl.CharacterFlip.IsFacingRight ? Vector2.right : Vector2.left;
        Vector2 pos = this.characterCtrl.FirePointDiagonal.transform.position;

        SkillProjSpawner.Instance.StartDiagonalBeamSkill(skillName, pos, dir, this.characterCtrl);
    }
    protected void EnableSpawnBeamFadeFXProj(BeamSkillNameEnum beamSkillName, float offsetX = 0, float offsetY = 0)
    {
        Vector2 pos = this.GetPosToSpawn(offsetX, offsetY);
        BeamFadeFXSpawner.Instance.Spawn(beamSkillName.ToString(), pos, this.characterCtrl);
    }

    protected void EnableSkillOne()
    {
        SuperFreezeManager.Instance?.EndSuperFreeze();
        EnableSpawnSkillProj(SkillNameEnum.Skill_1);
    }
    protected void EnableSkillTwo()
    {
        SuperFreezeManager.Instance?.EndSuperFreeze();
        EnableSpawnStraightBeamSkillProj(SkillNameEnum.Skill_2, this.skillSpawnOffsetX);
    }
    protected void EnableSkillUltimate()
    {
        SuperFreezeManager.Instance?.EndSuperFreeze();
        EnableSpawnStraightBeamSkillProj(SkillNameEnum.Skill_Ultimate, 1.95f);
    }
    protected void EnableBeamFadeFXProj() => EnableSpawnBeamFadeFXProj(BeamSkillNameEnum.Final_Flash, 7.35f);
    protected void EnableSpawnSkillBPAir()
    {
        SkillProjectile skillProjectile = EnableSpawnSkillProj(SkillNameEnum.Skill_BasicProjectile_Air);
        skillProjectile.transform.position = this.transform.position;
    }
    protected void EnableSpawnSkillBPGround()
    {
        SkillProjectile skillProjectile = EnableSpawnSkillProj(SkillNameEnum.Skill_BasicProjectile_Ground);
        skillProjectile.transform.position = this.characterCtrl.FirePoint.transform.position;
    }

    protected void EnableBurstSkill()
    {
        SkillProjectile skillProjectile = EnableSpawnSkillProj(SkillNameEnum.Skill_Burst);
        skillProjectile.transform.position = this.characterCtrl.transform.position;
    }

    protected Vector2 GetPosToSpawn(float posX = 0, float posY = 0)
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        newPos.x += this.characterCtrl.CharacterFlip.Direct * posX;
        newPos.y += this.characterCtrl.CharacterFlip.Direct * posY;
        return newPos;
    }
}
