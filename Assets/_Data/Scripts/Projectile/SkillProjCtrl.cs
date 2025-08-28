using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjCtrl : BaseBehaviour
{
    [Header("Skill Proj Ctrl")]
    [SerializeField] protected SkillProjectile skillProj;
    public SkillProjectile SkillProj => skillProj;
    [SerializeField] protected SkillProjSpawner skillProjSpawner;
    public SkillProjSpawner SkillProjSpawner => skillProjSpawner;
    [SerializeField] protected SkillProjDespawn skillProjDespawn;
    public SkillProjDespawn SkillProjDespawn => skillProjDespawn;
    [SerializeField] protected SkillDataSO skillData;
    public SkillDataSO SkillData => skillData;
    [SerializeField] protected BaseSkillDamageSender baseSkillDamageSender;
    public BaseSkillDamageSender BaseSkillDamageSender => baseSkillDamageSender;
    [Header("Owner")]
    [SerializeField] protected CharacterCtrl owner;
    public CharacterCtrl Owner { get => owner; set => owner = value; }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.skillProjDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjectile();
        this.LoadSkillProjectileSpawner();
        this.LoadSkillProjectileDespawn();
        this.LoadSkillProjectileData();
        this.LoadDamageSender();
    }
    protected void LoadSkillProjectile()
    {
        if (this.skillProj != null) return;
        this.skillProj = GetComponent<SkillProjectile>();
        Debug.Log(transform.name + ": LoadSkillProjectile", gameObject);
    }
    protected void LoadSkillProjectileSpawner()
    {
        if (this.skillProjSpawner != null) return;
        this.skillProjSpawner = transform.parent.GetComponentInParent<SkillProjSpawner>();
        Debug.Log(transform.name + ": LoadSkillProjectileSpawner", gameObject);
    }
    protected void LoadSkillProjectileDespawn()
    {
        if (this.skillProjDespawn != null) return;
        this.skillProjDespawn = GetComponentInChildren<SkillProjDespawn>();
        Debug.Log(transform.name + ": LoadSkillProjectileDespawn", gameObject);
    }
    protected void LoadSkillProjectileData()
    {
        if (this.skillData != null) return;
        foreach (SkillDataSO child in this.skillProjSpawner.SkillList)
        {
            if (child.skillName != this.skillProj.SkillName) continue;
            this.skillData = child;
            break;
        }
        Debug.Log(transform.name + ": LoadSkillProjectileData", gameObject);
    }
    protected void LoadDamageSender()
    {
        if (this.baseSkillDamageSender != null) return;
        this.baseSkillDamageSender = GetComponentInChildren<BaseSkillDamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }
}
