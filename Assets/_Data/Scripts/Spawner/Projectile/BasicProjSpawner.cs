using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjSpawner : Spawner<BasicProjectile>
{
    protected static BasicProjSpawner instance;
    public static BasicProjSpawner Instance => instance;
    public string basicProjName = "BasicProj";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 BasicProjSpawner allows to exists");
        instance = this;
    }
}

