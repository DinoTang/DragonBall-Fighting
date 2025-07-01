using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "SO/BeamSkillData")]
public class BeamSkillDataSO : SkillDataSO
{
    [Header("Beam Settings")]
    public GameObject beamHead;
    public GameObject beamTail;
    public float beamDuration = 2.0f;
    public float beamPartLength;
    public float beamSpawnInterval;
    public int beamMaxParts;
}
