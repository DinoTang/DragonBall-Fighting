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
    protected override void Start()
    {
        // base.Start();
        // this.currentKi = kiMax;
    }
    public void ChargeKi()
    {
        if (this.currentKi >= this.kiMax) return;
        float amount = Time.deltaTime * this.kiChargeRate;
        this.currentKi = Mathf.Clamp(this.currentKi + amount, 0, this.kiMax);
    }
    public float GetCurrentKi()
    {
        return currentKi;
    }
    public bool CanUseSkill(float kiCost)
    {
        if (this.currentKi - kiCost >= 0) return true;
        return false;
    }
    public void GainKi(float kiCost)
    {
        this.currentKi += kiCost;
        if (this.currentKi >= this.kiMax)
        {
            this.currentKi = this.kiMax;
        }
    }
    public void ConsumeKi(float kiCost)
    {
        this.currentKi -= kiCost;
    }
}
