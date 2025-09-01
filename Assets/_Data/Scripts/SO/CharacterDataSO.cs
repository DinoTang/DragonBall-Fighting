using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("CharacterData")]

    [Header("Sound")]
    public AudioClip startChargeClip;
    public AudioClip chargingClip;

}

