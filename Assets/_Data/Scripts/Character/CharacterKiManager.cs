using UnityEngine;
using System.Collections.Generic;

public class CharacterKiManager : CharacterAbstract
{
    [Header("Ki Manager")]
    [SerializeField] protected float kiMax = 300f;
    public float KiMax => kiMax;
    [SerializeField] protected float kiChargeRate = 20f;
    [SerializeField] protected float currentKi;
    public float CurrentKi => currentKi;
    [SerializeField] protected KiBalanceConfig balance;
    public KiBalanceConfig Balance => balance;
    public void ChargeKi()
    {
        if (this.currentKi >= this.kiMax) return;
        float amount = Time.deltaTime * this.kiChargeRate;
        this.currentKi = Mathf.Clamp(this.currentKi + amount, 0, this.kiMax);
    }

    public bool CanUseSkill(SkillDataSO skill)
    {
        if (skill == null) return false;
        float kiCost = this.GetSkillCost(skill);
        return (this.currentKi - kiCost >= 0);
    }
    public void ApplyKiChange(KiChangeReason reason, SkillDataSO skillDataSO = null)
    {
        float delta = 0;

        switch (reason)
        {
            case KiChangeReason.Hit:
                delta = this.balance.hitGain;
                break;
            case KiChangeReason.GetHit:
                delta = this.balance.hurtGain;
                break;
            case KiChangeReason.Block:
                delta = this.balance.blockGain;
                break;
            case KiChangeReason.SkillCast:
                delta = skillDataSO != null ? -this.GetSkillCost(skillDataSO) : 0;
                break;
            case KiChangeReason.BlockingSkill:
                delta = -this.balance.guardKiCost;
                break;
        }

        this.currentKi = Mathf.Clamp(this.currentKi + delta, 0, this.kiMax);
    }

    protected float GetSkillCost(SkillDataSO skill)
    {
        if (skill == null) return 0;
        switch (skill.skillType)
        {
            case SkillType.Normal: return balance.normalSkillCost;
            case SkillType.Medium: return balance.mediumSkillCost;
            default: return balance.strongSkillCost;
        }
    }


}
