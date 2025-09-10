using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AnnouncerSoundData", menuName = "SO/Sound/Announcer")]
public class AnnouncerSoundSO : ScriptableObject
{
    [Header("AnnouncerSoundSO")]
    public List<AnnouncerSoundEntry> sounds;
}