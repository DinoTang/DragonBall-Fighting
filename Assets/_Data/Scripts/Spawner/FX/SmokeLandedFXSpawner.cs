using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeLandedFXSpawner : Spawner<SmokeLandedFX>
{
    protected static SmokeLandedFXSpawner instance;
    public static SmokeLandedFXSpawner Instance => instance;
    public string nameFX = "SmokeLandedFX";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 SmokeLandedFXSpawner allows to exist");
        instance = this;
    }
}
