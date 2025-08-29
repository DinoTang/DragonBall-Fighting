using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : PoolObj
{
    [SerializeField] protected SkillNameEnum skillName;
    public SkillNameEnum SkillName => skillName;
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    public SkillProjCtrl SkillProjCtrl => skillProjCtrl;
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
        return this.skillName.ToString();
    }






}
