using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class BaseSkillDamageSender : DamageSender
{
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
        this.ResetSkillDamageValue();
    }
    public void ResetSkillDamageValue()
    {
        this.damage = this.skillProjCtrl.SkillData.damage;
    }
    protected void LoadSkillProjCtrl()
    {
        if (this.skillProjCtrl != null) return;
        this.skillProjCtrl = GetComponentInParent<SkillProjCtrl>();
        Debug.Log(transform.name + ": LoadSkillProjCtrl", gameObject);
    }
    
}