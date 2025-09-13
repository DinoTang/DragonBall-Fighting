using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharactersDataSO", menuName = "SO/Character/CharactersDataSO")]
public class CharactersDataSO : ScriptableObject
{
    [System.Serializable]
    public class CharacterEntry
    {
        public CharacterEnum characterName;
        public CharacterDataSO characterSO;
    }

    public List<CharacterEntry> characters;

    public CharacterDataSO GetCharacter(CharacterEnum characterName)
    {
        return characters.Find(c => c.characterName == characterName)?.characterSO;
    }
}
