using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlue : PlayerType
{
    protected static PlayerBlue instance;
    public static PlayerBlue Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (PlayerBlue.instance != null) Debug.LogWarning("Only 1 PlayerBlue allows to exist");
        instance = this;
    }
}
