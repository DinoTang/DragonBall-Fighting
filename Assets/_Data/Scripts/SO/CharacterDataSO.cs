using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("CharacterData")]

    [Header("Sound")]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip dodgeHitClip;
    public AudioClip dodgeKickClip;
    public AudioClip weakHitClip;
    public AudioClip strongHitClip;
    public AudioClip groundHitClip;
    public AudioClip bounceGroundHitClip;
    public AudioClip landingClip;
    public AudioClip startChargeClip;
    public AudioClip chargingClip;

}

