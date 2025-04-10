using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : DinoBehaviour
{
    public virtual EnergyShot Spawn(EnergyShot energyShot_prefab, Vector2 pos, Quaternion rot)
    {
        EnergyShot new_energyShot = Instantiate(energyShot_prefab);
        new_energyShot.transform.SetPositionAndRotation(pos, rot);
        return new_energyShot;
    }
    public virtual EnergyShotFX Spawn(EnergyShotFX energyShot_prefab, Vector2 pos, Quaternion rot)
    {
        EnergyShotFX new_energyShotFX = Instantiate(energyShot_prefab);
        new_energyShotFX.transform.SetPositionAndRotation(pos, rot);
        return new_energyShotFX;
    }
    public virtual Transform Spawn(Transform prefab)
    {
        Transform new_Prefab = Instantiate(prefab);
        return new_Prefab;
    }

}
