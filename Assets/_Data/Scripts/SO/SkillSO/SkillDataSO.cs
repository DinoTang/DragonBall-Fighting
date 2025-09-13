using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/SkillData")]
public class SkillDataSO : ScriptableObject
{
    [Header("Base SkillDataSO")]
    public SkillNameEnum skillName;
    public ExplosionFXEnum explosionFXName;
    public float kiCost;
    public float cooldown;
    public float damage;
    public AnimationClip animationClip;
    public AudioClip sfx;
    public virtual CharacterState GetSkillState()
    {
        return new EnergySkillState();
    }

}

