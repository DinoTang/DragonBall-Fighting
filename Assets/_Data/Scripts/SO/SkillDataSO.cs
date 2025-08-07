using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public SkillSlot skillName;
    public ExplosionFXEnum damageFxName;
    public float kiCost;
    public float cooldown;
    public float damage;
    public AnimationClip animationClip;
    public AudioClip sfx;
    public virtual State GetSkillState()
    {
        return new EnergyProjectileSkillState();
    }

}

