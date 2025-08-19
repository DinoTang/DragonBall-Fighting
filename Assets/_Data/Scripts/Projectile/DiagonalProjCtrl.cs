using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalProjCtrl : DinoBehaviour
{
    [SerializeField] protected DiagonalProjectile diagonalProj;
    public DiagonalProjectile DiagonalProj => diagonalProj;
    [SerializeField] protected DiagonalProjDespawn diagonalProjDespawn;
    public DiagonalProjDespawn DiagonalProjDespawn => diagonalProjDespawn;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.diagonalProjDespawn.ResetCurrentTime();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDiagonalProj();
        this.LoadBasicProjDespawn();
    }
    protected void LoadDiagonalProj()
    {
        if (this.diagonalProj != null) return;
        this.diagonalProj = GetComponent<DiagonalProjectile>();
        Debug.Log(transform.name + ": LoadBasicProj", gameObject);
    }
    protected void LoadBasicProjDespawn()
    {
        if (this.diagonalProjDespawn != null) return;
        this.diagonalProjDespawn = GetComponentInChildren<DiagonalProjDespawn>();
        Debug.Log(transform.name + ": LoadBasicProjDespawn", gameObject);
    }
}
