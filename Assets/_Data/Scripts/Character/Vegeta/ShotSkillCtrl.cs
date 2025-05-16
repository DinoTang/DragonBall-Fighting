using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSkillCtrl : DinoBehaviour
{
    [SerializeField] protected ShotSkillSpawner shotSkillSpawner;
    public ShotSkillSpawner ShotSkillSpawner => shotSkillSpawner;
    [SerializeField] protected ShotSkillDespawn shotSkillDespawn;
    public ShotSkillDespawn ShotSkillDespawn => shotSkillDespawn;
    [SerializeField] protected ShotSkill shotSkill;
    public ShotSkill ShotSkill => shotSkill;
    [SerializeField] protected SkillDataSO shotSkillData;
    public SkillDataSO ShotSkillData => shotSkillData;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.shotSkillDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadShotSkillSpawner();
        this.LoadShotSkillDespawn();
        this.LoadShotSkill();
        this.LoadShotSkillData();
    }
    protected void LoadShotSkill()
    {
        if (this.shotSkill != null) return;
        this.shotSkill = GetComponent<ShotSkill>();
        Debug.Log(transform.name + ": LoadShotSkill", gameObject);
    }
    protected void LoadShotSkillSpawner()
    {
        if (this.shotSkillSpawner != null) return;
        this.shotSkillSpawner = transform.parent.GetComponentInParent<ShotSkillSpawner>();
        Debug.Log(transform.name + ": LoadShotSkillSpawner", gameObject);
    }
    protected void LoadShotSkillDespawn()
    {
        if (this.shotSkillDespawn != null) return;
        this.shotSkillDespawn = GetComponentInChildren<ShotSkillDespawn>();
        Debug.Log(transform.name + ": LoadshotSkillDespawn", gameObject);
    }
    protected void LoadShotSkillData()
    {
        if (this.shotSkillData != null) return;
        int index_SkillSlot = (int)this.shotSkill.SkillSlot - 1;
        if (index_SkillSlot < 0 || index_SkillSlot >= this.shotSkillSpawner.SkillList.Count) return;

        this.shotSkillData = this.shotSkillSpawner.SkillList[index_SkillSlot];
        Debug.Log(transform.name + ": LoadShotSkillData", gameObject);
    }

}
