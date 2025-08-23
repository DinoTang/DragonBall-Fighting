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

    protected void EnableSpawnBasicProjectileEffect()
    {
        BasicProjFXSpawner.Instance.Spawn(this.characterCtrl.CharacterName.ToString(), this.GetPosToSpawn(), this.characterCtrl);
    }

    protected SkillProjectile EnableSpawnSkillProj(SkillName skillName)
    {
        SkillProjectile skill = SkillProjSpawner.Instance.GetSkill(skillName);
        if (skill == null)
        {
            Debug.LogWarning($"{transform.parent.name}: Không tìm thấy SkillProjectile cho {skillName}");
            return null;
        }

        return SkillProjSpawner.Instance.Spawn(skill, this.GetPosToSpawn(this.skillSpawnOffsetX), this.characterCtrl);
    }

    protected void EnableSpawnStraightBeamSkillProj(SkillName skillName, float offsetX = 0, float offsetY = 0)
    {
        Vector2 pos = this.GetPosToSpawn(offsetX, offsetY);
        Vector2 dir = this.characterCtrl.CharacterFlip.IsFacingRight ? Vector2.right : Vector2.left;

        SkillProjSpawner.Instance.StartStraightBeamSkill(skillName, pos, dir, this.characterCtrl);
    }
    protected void EnableSpawnDiagonalBeamSkillProj(SkillName skillName)
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

    protected void EnableSkillOne() => EnableSpawnSkillProj(SkillName.Skill_1);
    protected void EnableSkillTwo() => EnableSpawnStraightBeamSkillProj(SkillName.Skill_2, this.skillSpawnOffsetX);
    protected void EnableSkillUltimate() => EnableSpawnStraightBeamSkillProj(SkillName.Skill_Ultimate, 1.95f);
    protected void EnableBeamFadeFXProj() => EnableSpawnBeamFadeFXProj(BeamSkillNameEnum.Final_Flash, 7.35f);
    protected void EnableSpawnSkillBPAir()
    {
        SkillProjectile skillProjectile = EnableSpawnSkillProj(SkillName.Skill_BasicProjectile_Air);
        skillProjectile.transform.position = this.transform.position;
    }
    protected void EnableSpawnSkillBPGround()
    {
        SkillProjectile skillProjectile = EnableSpawnSkillProj(SkillName.Skill_BasicProjectile_Ground);
        skillProjectile.transform.position = this.characterCtrl.FirePoint.transform.position;
    }
    protected Vector2 GetPosToSpawn(float posX = 0, float posY = 0)
    {
        Vector2 newPos = this.characterCtrl.FirePoint.transform.position;
        newPos.x += this.characterCtrl.CharacterFlip.Direct * posX;
        newPos.y += this.characterCtrl.CharacterFlip.Direct * posY;
        return newPos;
    }



    protected void PlayCutScene()
    {

    }
}
