using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("CharacterData")]

    [Header("Sound")]
    public AudioClip moveClip;
    public AudioClip jumpClip;
    public AudioClip GroundHitClip;
    public AudioClip BounceGroundHitClip;
    public AudioClip landingClip;
    public AudioClip startChargeClip;
    public AudioClip chargingClip;

}

