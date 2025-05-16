using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public string skillName;
    public string fxName;
    public float kiCost;
    public float cooldown;
    public float damage;
    public AnimationClip animation;
    public AudioClip sfx;
}
