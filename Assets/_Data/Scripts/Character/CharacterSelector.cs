using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : DinoBehaviour
{
    [SerializeField] protected CharacterEnum selectedCharacter;

    public string GetName()
    {
        return this.selectedCharacter.ToString();
    }
}
