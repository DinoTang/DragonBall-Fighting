using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public SkillSlot skillName;
    public ExplosionFXEnum damageFxName;
    public float kiCost;
    public float cooldown;
    public float damage;
    public AnimationClip animation;
    public AudioClip sfx;
}
