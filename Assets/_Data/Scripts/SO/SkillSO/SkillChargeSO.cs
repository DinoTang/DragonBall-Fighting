using UnityEngine;
[CreateAssetMenu(fileName = "SkillChargeSO", menuName = "SO/Skill/SkillChargeSO")]
public class SkillChargeSO : SkillDataSO
{
    [Header("Skill Charge SO")]
    public AudioClip SFXCharge;
    public override CharacterState GetSkillState()
    {
        return new EnergySkillChargeState();
    }

}

