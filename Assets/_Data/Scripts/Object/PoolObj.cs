using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PoolObj : DinoBehaviour
{
    [SerializeField] protected Model model;
    public abstract string GetName();
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadModel();
    }
    protected void LoadModel()
    {
        if (this.model != null) return;
        this.model = GetComponentInChildren<Model>();
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }

    public virtual void Init(CharacterCtrl owner)
    {
        if (owner.transform.localScale.x == -1)
        {
            this.model.transform.localScale = new Vector3(-1, 1, 1) * this.model.Size;
        }
        else
        {
            this.model.transform.localScale = new Vector3(1, 1, 1) * this.model.Size;
        }
    }
}
