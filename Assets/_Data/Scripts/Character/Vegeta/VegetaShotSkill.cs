using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaShotSkill : EnergyShot
{
    [Header("Vegeta Shot Skill")]
    [SerializeField] protected SkillLevelEnum skillLevel;
    public override string GetName()
    {
        return this.skillLevel.ToString();
    }
}
