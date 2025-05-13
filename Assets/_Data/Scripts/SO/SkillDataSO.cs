using UnityEngine;
[CreateAssetMenu(fileName = "SkillData",menuName = "SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
   public string skillName;
    public float kiCost;
    public float cooldown;
    public AnimationClip animation;
    public GameObject vfxPrefab;
}
