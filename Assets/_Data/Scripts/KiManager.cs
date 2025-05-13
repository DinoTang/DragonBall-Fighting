using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KiManager : CharacterAbstract
{
    [Header("Ki Manager")]
    [SerializeField] protected float totalKi = 300f;
    [SerializeField] protected float kiChargeRate = 20f;
    [SerializeField] protected float currentKi;
    public void ChargeKi()
    {
        if (this.currentKi >= this.totalKi) return;
        float amount = Time.deltaTime * this.kiChargeRate;
        this.currentKi = Mathf.Clamp(this.currentKi + amount, 0, this.totalKi);
    }

    public bool CanUseSkill(float kiCost)
    {
        if (this.currentKi - kiCost >= 0) return true;
        return false;
    }

    public void ConsumeKi(float kiCost)
    {
        this.currentKi -= kiCost;
    }
}
