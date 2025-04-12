using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShot : PoolObj
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected CharacterCtrl owner;
    [SerializeField] protected Model model;
    [SerializeField] protected Vector2 direction;
    public override string GetName()
    {
        return "EnergyShot_Goku";
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadModel();
        this.LoadOwner();
    }
    protected void LoadModel()
    {
        if (this.model != null) return;
        this.model = GetComponentInChildren<Model>();
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected void LoadOwner()
    {

    }
    protected void Update()
    {
        this.transform.Translate(this.speed * Time.deltaTime * this.direction);
    }
    public void Init(CharacterCtrl owner)
    {
        this.owner = owner;

        if (this.owner.transform.localScale.x == -1)
        {
            this.direction = Vector2.left;
            this.model.transform.localScale = new Vector3(-1, 1, 1) * this.model.Size;
        }
        else
        {
            this.direction = Vector2.right;
            this.model.transform.localScale = new Vector3(1, 1, 1) * this.model.Size;
        }
    }

}
