using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : _ProjectileBase
{
    [Header("Skill Projectile")]
    [SerializeField] protected SkillSlot skillSlot;
    public SkillSlot SkillSlot => skillSlot;
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
    }
    protected void LoadSkillProjCtrl()
    {
        if (this.skillProjCtrl != null) return;
        this.skillProjCtrl = GetComponent<SkillProjCtrl>();
        Debug.Log(transform.name + ": LoadSkillProjCtrl");
    }
    public override string GetName()
    {
        return this.skillSlot.ToString();
    }
}
