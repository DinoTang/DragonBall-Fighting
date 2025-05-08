using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotFXSpawner : Spawner<EnergyShotFX>
{
    protected static EnergyShotFXSpawner instance;
    public static EnergyShotFXSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 EnergyShotFXSpawner allows to exists");
        instance = this;
    }
}
