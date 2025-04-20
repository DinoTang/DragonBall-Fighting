using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleFX_Spawner : Spawner<TeleFX>
{
    protected static TeleFX_Spawner instance;
    public static TeleFX_Spawner Instance => instance;
    public string nameFX = "TeleFX";
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 TeleFX_Spawner allows to exist");
        instance = this;
    }
}
