using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjSpawner : Spawner<SkillProjectile>
{
    protected static SkillProjSpawner instance;
    public static SkillProjSpawner Instance => instance;
    [Header("Shot Skill Spawner")]
    [SerializeField] protected CharacterSelector characterSelector;
    [SerializeField] protected List<SkillDataSO> skillList;
    public List<SkillDataSO> SkillList => skillList;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 ShotSkillSpawner allows to exists");
        instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterSelector();
        this.LoadSkillList();
    }
    protected void LoadCharacterSelector()
    {
        if (this.characterSelector != null) return;
        this.characterSelector = GetComponent<CharacterSelector>();
        Debug.Log(transform.name + ": LoadCharacterSelector", gameObject);
    }
    protected void LoadSkillList()
    {
        string path = $"SkillData/{this.characterSelector.GetName()}";
        SkillDataSO[] skillDatas = Resources.LoadAll<SkillDataSO>(path);
        this.skillList = new List<SkillDataSO>(skillDatas);
        Debug.Log(transform.name + ": LoadSkillList", gameObject);
    }
    public string GetSkill(SkillSlot skillSlot)
    {
        string skillLevel = skillSlot.ToString();
        foreach (SkillProjectile skillProj in this.prefabs)
        {
            if (skillProj.GetName() != skillLevel) continue;
            return skillProj.GetName();
        }
        return "";
    }
}
