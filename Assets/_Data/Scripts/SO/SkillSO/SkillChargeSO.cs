using UnityEngine;
[CreateAssetMenu(fileName = "SkillChargeSO", menuName = "SO/Skill/SkillChargeSO")]
public class SkillChargeSO : SkillDataSO
{
    [Header("Skill Charge SO")]
    public AudioClip SFXCharge;
    [Header("Super Freeze")]
    [SerializeField] protected bool hasSuperFreeze = true;
    public override bool HasSuperFreeze => hasSuperFreeze;
    public override CharacterState GetSkillState()
    {
        return new EnergySkillChargeState();
    }

}

