using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/BeamSkillData")]
public class BeamSkillDataSO : SkillDataSO
{
    [Header("Beam Settings")]
    public AnimationClip endBeamSkillAnim;
    public GameObject beamHead;
    public GameObject beamTail;
    public float duration;
    public float beamSpawnInterval;
    public int beamMaxParts;
    public float beamPartLength;
    public override State GetSkillState()
    {
        return new BeamSkillState();
    }
}
