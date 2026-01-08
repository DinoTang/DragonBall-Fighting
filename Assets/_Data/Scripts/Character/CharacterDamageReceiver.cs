using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterDamageReceiver : DamageReceiver
{
    [Header("Character Damage Receiver")]
    [SerializeField] protected CharacterCtrl characterCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
    }

    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
    }
    protected override void OnHurt()
    {
        base.OnHurt();
        if (this.characterCtrl.StateManager.CurrentState is KnockBackState) return;
        if (this.characterCtrl.StateManager.mainStateType is AirState)
            this.characterCtrl.StateManager.SetNextState(new AirHurtState());
        else this.characterCtrl.StateManager.SetNextState(new HurtState());
        this.characterCtrl.CharacterKiManager.ApplyKiChange(KiChangeReason.GetHit);
    }

    public void OnBlock()
    {
        this.OnParryFX();
        this.characterCtrl.CharacterKiManager.ApplyKiChange(KiChangeReason.Block);
    }
    public void OnParryFX()
    {
        Vector2 pos = this.characterCtrl.transform.position;
        pos.x += this.characterCtrl.CharacterFlip.Direct * 0.5f;
        pos.y -= 0.5f;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Parry.ToString(), pos, this.characterCtrl);
    }

    public void ToggleIgnoreEnemy(bool ignore)
    {
        Collider2D playerCol = this.collide;
        Collider2D enemyCol = this.characterCtrl.Opponent.CharacterDamageReceiver.collide;

        Physics2D.IgnoreCollision(playerCol, enemyCol, ignore);
    }
    protected override bool TryHandleDamage()
    {
        if (!this.isBlock) return false;
        this.OnBlock();
        return true;
    }
    protected bool TryHandleDamageBySkill(SkillDataSO skill)
    {
        if (!this.isBlock) return false;

        if (skill.skillType == SkillType.Normal)
        {
            this.OnBlock();
            this.ApplyDamageBySkillWhenBlock(skill);
            return true;
        }

        CharacterKiManager kiManager = this.characterCtrl.CharacterKiManager;
        if (skill.skillType == SkillType.Medium || skill.skillType == SkillType.Hard)
        {
            if (kiManager.CurrentKi - kiManager.Balance.guardKiCost >= 0)
            {
                kiManager.ApplyKiChange(KiChangeReason.BlockingSkill, skill);
                this.OnBlock();
                this.ApplyDamageBySkillWhenBlock(skill);
                return true;
            }

            return false; // guard break
        }

        return false;
    }

    public bool DeductHPBySkill(SkillDataSO skill, float hp)
    {
        if (!this.CanReceiveDamage()) return false;
        if (this.TryHandleDamageBySkill(skill)) return false;
        this.TakeDamage(hp);
        return true;
    }

    protected void ApplyDamageBySkillWhenBlock(SkillDataSO skill)
    {
        this.ApplyDamage(skill.damage * skill.blockDamageMultiplier);
    }
}
