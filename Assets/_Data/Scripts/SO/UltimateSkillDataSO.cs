using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/UltimateSkillData")]
public class UltimateSkillDataSO : BeamSkillDataSO
{
    [Header("Ultimate SkillDataSO")]
    public AnimationClip rushAttackUltimateAnim;
    public AnimationClip readyUltimateAnim;
    public AnimationClip startUltimateAnim;
    public override State GetSkillState()
    {
        return new UltimateSkillState();
    }

}

