using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFXSpawner : Spawner<ExplosionFX>
{
    protected static ExplosionFXSpawner instance;
    public static ExplosionFXSpawner Instance => instance;
    public static string efx_DefaultName = "Default_Explosion";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 ExplosionFXSpawner allows to exists");
        instance = this;
    }
}
