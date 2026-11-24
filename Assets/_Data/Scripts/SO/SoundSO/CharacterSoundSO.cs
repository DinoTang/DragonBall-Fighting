using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterSoundData", menuName = "SO/Sound/Character")]
public class CharacterSoundSO : ScriptableObject
{
    [Header("CharacterSoundSO")]
    public List<CharacterSoundEntryConfig> sounds;
}
