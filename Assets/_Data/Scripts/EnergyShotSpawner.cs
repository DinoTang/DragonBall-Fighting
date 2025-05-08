using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotSpawner : Spawner<EnergyShot>
{
    protected static EnergyShotSpawner instance;
    public static EnergyShotSpawner Instance => instance;
    public string energyShotName = "EnergyShot";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 EnergyShotSpawner allows to exists");
        instance = this;
    }
}

