using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/UltimateSkillData")]
public class UltimateSkillDataSO : BeamSkillDataSO
{
    [Header("Ultimate SkillDataSO")]
    public AnimationClip readyUltimateAnim;
    public AnimationClip cutSceneReadyUltimateAnim;
    public AnimationClip performUltimateAnim;
    public AnimationClip cutScenePerformUltimateAnim;
    public override CharacterState GetSkillState()
    {
        return new UltimateSkillState();
    }

}

