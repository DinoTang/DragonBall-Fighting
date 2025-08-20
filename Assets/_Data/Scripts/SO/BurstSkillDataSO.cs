using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/Skill/BurstSkillDataSO")]
public class BurstSkillDataSO : SkillDataSO
{
    public override State GetSkillState()
    {
        return new BurstSkillState();
    }
}
