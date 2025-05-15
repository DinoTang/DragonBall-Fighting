using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaShotSkillSpawner : Spawner<VegetaShotSkill>
{
    protected static VegetaShotSkillSpawner instance;
    public static VegetaShotSkillSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 VegetaShotSkillSpawner allows to exists");
        instance = this;
    }

    public string GetSkill(SkillLevelEnum skillLevelEnum)
    {
        string skillLevel = skillLevelEnum.ToString();
        foreach (VegetaShotSkill vegetaShotSkill in this.prefabs)
        {
            if (vegetaShotSkill.GetName() != skillLevel) continue;
            return vegetaShotSkill.GetName();
        }
        return "";
    }
}
