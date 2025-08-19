using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalProjSpawner : Spawner<DiagonalProjectile>
{
    protected static DiagonalProjSpawner instance;
    public static DiagonalProjSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 DiagonalProjSpawner allows to exists");
        instance = this;
    }
}
