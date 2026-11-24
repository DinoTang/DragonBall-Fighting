using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterSelectSO", menuName = "SO/Character/CharacterSelectSO")]
public class CharacterSelectSO : ScriptableObject
{
    [Header("Red Player")]
    public CharacterEnum redCharacterName;

    [Header("Blue Player")]
    public CharacterEnum blueCharacterName;
}
