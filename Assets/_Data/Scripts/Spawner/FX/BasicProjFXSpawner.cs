using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjFXSpawner : Spawner<BasicProjectileFX>
{
    protected static BasicProjFXSpawner instance;
    public static BasicProjFXSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 BasicProjFXSpawner allows to exists");
        instance = this;
    }
}
