using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ProjectileBase : PoolObj
{
    [Header("Projectile Base")]
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected CharacterCtrl owner;
    public CharacterCtrl Owner => owner;
    public override string GetName()
    {
        return "";
    }
    protected void Update()
    {
        this.transform.Translate(this.speed * Time.deltaTime * this.direction);
    }

    public override void Init(CharacterCtrl owner)
    {
        this.owner = owner;
        if (owner.transform.localScale.x == -1)
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
