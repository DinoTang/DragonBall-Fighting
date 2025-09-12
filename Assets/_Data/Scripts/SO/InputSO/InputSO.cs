using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input")]
public class InputSO : ScriptableObject
{
    public PlayerInputConfig playerRedKeys;
    public PlayerInputConfig playerBlueKeys;
}