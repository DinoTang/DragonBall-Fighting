using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UISoundData", menuName = "SO/Sound/UI")]
public class UISoundSO : ScriptableObject
{
    [Header("UISoundSO")]
    public List<UISoundEntryConfig> sounds;
}
