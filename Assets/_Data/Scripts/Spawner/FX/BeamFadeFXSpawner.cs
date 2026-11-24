using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamFadeFXSpawner : Spawner<BeamFadeFX>
{
    protected static BeamFadeFXSpawner instance;
    public static BeamFadeFXSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 BeamFadeFXSpawner allows to exists");
        instance = this;
    }
}
