using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    [Header("Base SkillDataSO")]
    public SkillNameEnum skillName;
    public SkillType skillType;
    public ExplosionFXEnum explosionFXName;
    public float cooldown;
    public float damage;
    public float blockDamageMultiplier = 0.4f;
    public AnimationClip animationClip;
    public AudioClip sfx;
    public virtual CharacterState GetSkillState()
    {
        return new EnergySkillState();
    }

}

