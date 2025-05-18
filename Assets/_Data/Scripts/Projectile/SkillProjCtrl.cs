using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjCtrl : DinoBehaviour
{
    [SerializeField] protected SkillProjectile skillProj;
    public SkillProjectile SkillProj => skillProj;
    [SerializeField] protected SkillProjSpawner skillProjSpawner;
    public SkillProjSpawner SkillProjSpawner => skillProjSpawner;
    [SerializeField] protected SkillProjDespawn skillProjDespawn;
    public SkillProjDespawn SkillProjDespawn => skillProjDespawn;
    [SerializeField] protected SkillDataSO shotSkillData;
    public SkillDataSO ShotSkillData => shotSkillData;
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
        if (this.shotSkillData != null) return;
        int index_SkillSlot = (int)this.skillProj.SkillSlot - 1;
        if (index_SkillSlot < 0 || index_SkillSlot >= this.skillProjSpawner.SkillList.Count) return;

        this.shotSkillData = this.skillProjSpawner.SkillList[index_SkillSlot];
        Debug.Log(transform.name + ": LoadSkillProjectileData", gameObject);
    }

}
