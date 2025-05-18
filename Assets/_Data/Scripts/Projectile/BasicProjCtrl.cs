using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjCtrl : DinoBehaviour
{
    [SerializeField] protected BasicProjectile basicProj;
    public BasicProjectile BasicProj => basicProj;
    [SerializeField] protected BasicProjDespawn basicProjDespawn;
    public BasicProjDespawn BasicProjDespawn => basicProjDespawn;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.basicProjDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBasicProj();
        this.LoadBasicProjDespawn();
    }
    protected void LoadBasicProj()
    {
        if (this.basicProj != null) return;
        this.basicProj = GetComponent<BasicProjectile>();
        Debug.Log(transform.name + ": LoadBasicProj", gameObject);
    }
    protected void LoadBasicProjDespawn()
    {
        if (this.basicProjDespawn != null) return;
        this.basicProjDespawn = GetComponentInChildren<BasicProjDespawn>();
        Debug.Log(transform.name + ": LoadBasicProjDespawn", gameObject);
    }
}
