using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeJumpFXSpawner : Spawner<SmokeJumpFX>
{
    protected static SmokeJumpFXSpawner instance;
    public static SmokeJumpFXSpawner Instance => instance;
    public string nameFX = "SmokeJumpFX";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 SmokeJumpFXSpawner allows to exist");
        instance = this;
    }
}
