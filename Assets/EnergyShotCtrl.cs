using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotCtrl : DinoBehaviour
{
    [SerializeField] protected EnergyShotDespawn energyShotDespawn;
    public EnergyShotDespawn EnergyShotDespawn => energyShotDespawn;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnergyShotDespawn();
    }

    protected void LoadEnergyShotDespawn()
    {
        if (this.energyShotDespawn != null) return;
        this.energyShotDespawn = GetComponentInChildren<EnergyShotDespawn>();
        Debug.Log(transform.name + ": LoadEnergyShotDespawn", gameObject);
    }
}
