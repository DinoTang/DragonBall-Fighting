using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotCtrl : DinoBehaviour
{
    [SerializeField] protected EnergyShot energyShot;
    public EnergyShot EnergyShot => energyShot;
    [SerializeField] protected EnergyShotDespawn energyShotDespawn;
    public EnergyShotDespawn EnergyShotDespawn => energyShotDespawn;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.energyShotDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnergyShot();
        this.LoadEnergyShotDespawn();
    }
    protected void LoadEnergyShot()
    {
        if (this.energyShot != null) return;
        this.energyShot = GetComponent<EnergyShot>();
        Debug.Log(transform.name + ": LoadEnergyShot", gameObject);
    }
    protected void LoadEnergyShotDespawn()
    {
        if (this.energyShotDespawn != null) return;
        this.energyShotDespawn = GetComponentInChildren<EnergyShotDespawn>();
        Debug.Log(transform.name + ": LoadEnergyShotDespawn", gameObject);
    }
}
