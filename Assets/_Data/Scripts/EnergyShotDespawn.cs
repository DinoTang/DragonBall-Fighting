using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotDespawn : Despawn<EnergyShot>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.lifeTime = 4;
    }
    public void ResetCurrentTime()
    {
        this.currentTime = this.lifeTime;
    }
}
