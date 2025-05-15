using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaShotSkillCtrl : DinoBehaviour
{
    [SerializeField] protected VegetaShotSkill vegetaShotSkill;
    public VegetaShotSkill VegetaShotSkill => vegetaShotSkill;
    [SerializeField] protected VegetaShotSkillDespawn vegetaShotSkillDespawn;
    public VegetaShotSkillDespawn VegetaShotSkillDespawn => vegetaShotSkillDespawn;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.vegetaShotSkillDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadVegetaShotSkill();
        this.LoadVegetaShotSkillDespawn();
    }
    protected void LoadVegetaShotSkill()
    {
        if (this.vegetaShotSkill != null) return;
        this.vegetaShotSkill = GetComponent<VegetaShotSkill>();
        Debug.Log(transform.name + ": LoadVegetaShotSkill", gameObject);
    }
    protected void LoadVegetaShotSkillDespawn()
    {
        if (this.vegetaShotSkillDespawn != null) return;
        this.vegetaShotSkillDespawn = GetComponentInChildren<VegetaShotSkillDespawn>();
        Debug.Log(transform.name + ": LoadVegetaShotSkillDespawn", gameObject);
    }
}
