using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyKiChargeFXSpawner : Spawner<ReadyKiChargeFX>
{
    protected static ReadyKiChargeFXSpawner instance;
    public static ReadyKiChargeFXSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 ReadyKiChargeFXSpawner allows to exist");
        instance = this;
    }
}

