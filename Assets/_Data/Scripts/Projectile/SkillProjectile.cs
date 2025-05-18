using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : _ProjectileBase
{
    [Header("Skill Projectile")]
    [SerializeField] protected SkillSlot skillSlot;
    public SkillSlot SkillSlot => skillSlot;
    public override string GetName()
    {
        return this.skillSlot.ToString();
    }
}
