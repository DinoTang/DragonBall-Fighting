using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/Character/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("CharacterData")]
    public CharacterEnum characterName;
    public Sprite portrait;
    public RuntimeAnimatorController animator;
    public AnimationClip readyClip;
    public List<SkillDataSO> skillList = new();

    public AudioClip voiceChar;

}

