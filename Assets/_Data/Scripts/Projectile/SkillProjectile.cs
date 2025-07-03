using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillProjectile : _ProjectileBase
{
    [Header("Skill Projectile")]
    [SerializeField] protected SkillSlot skillSlot;
    public SkillSlot SkillSlot => skillSlot;
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    [SerializeField] protected float skillDuration;
    public float SKillDuration => skillDuration;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
        this.LoadSkillDuration();
    }
    protected void LoadSkillProjCtrl()
    {
        if (this.skillProjCtrl != null) return;
        this.skillProjCtrl = GetComponent<SkillProjCtrl>();
        Debug.Log(transform.name + ": LoadSkillProjCtrl");
    }
    protected void LoadSkillDuration()
    {
        if (!this.skillProjCtrl) return;
        this.skillDuration = this.skillProjCtrl.SkillData.duration;
        Debug.Log(transform.name + ": LoadSkillDuration");
    }
    public override string GetName()
    {
        return this.skillSlot.ToString();
    }
}
