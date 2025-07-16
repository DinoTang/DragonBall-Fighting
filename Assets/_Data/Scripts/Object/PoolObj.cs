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
        if (owner == null || this.model == null) return;

        bool isLeft = false;

        if (!owner.CharacterFlip.IsFacingRight)
        {
            isLeft = true;
        }

        FlipModelX(isLeft);
    }

    protected void FlipModelX(bool isLeft)
    {
        if (this.model == null) return;

        Vector3 scale = this.model.transform.localScale;
        scale.x = Mathf.Abs(scale.x);

        if (isLeft)
        {
            scale.x *= -1;
        }

        this.model.transform.localScale = scale;
    }

}
