using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/BeamSkillData")]
public class BeamSkillDataSO : SkillChargeSO
{
    [Header("Beam Settings")]
    public AnimationClip endBeamSkillAnim;
    public GameObject beamHead;
    public GameObject beamTail;
    public float duration;
    public float beamSpawnInterval;
    public int beamMaxParts;
    public float beamHeadLength;
    public float beamBodyLength;
    public float beamTailLength;
    public override CharacterState GetSkillState()
    {
        return new BeamSkillState();
    }
}
