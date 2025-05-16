using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSkill : EnergyShot
{
    [Header("Shot Skill")]
    [SerializeField] protected SkillSlot skillSlot;
    public SkillSlot SkillSlot => skillSlot;
    public override string GetName()
    {
        return this.skillSlot.ToString();
    }
}
