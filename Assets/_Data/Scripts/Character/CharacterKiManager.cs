using UnityEngine;
using UnityEngine.UI;
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
        float kiCost = this.GetSkillCost(skill);
        if (this.currentKi - kiCost >= 0) return true;
        return false;
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
                delta = -this.GetSkillCost(skillDataSO);
                break;
            case KiChangeReason.BlockingSkill:
                delta = -this.balance.guardKiCost;
                break;
        }

        if (this.currentKi >= this.kiMax && delta > 0)
        {
            this.currentKi = this.kiMax;
            return;
        }
        this.currentKi += delta;
    }

    protected float GetSkillCost(SkillDataSO skill)
    {
        switch (skill.skillType)
        {
            case SkillType.Normal: return balance.normalSkillCost;
            case SkillType.Medium: return balance.mediumSkillCost;
            default: return balance.strongSkillCost;
        }
    }


}
