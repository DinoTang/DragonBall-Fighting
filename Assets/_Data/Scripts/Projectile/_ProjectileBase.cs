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

        if (owner.transform.localScale.x < 0)
        {
            this.direction = Vector2.left;
            FlipModelX(true);
        }
        else
        {
            this.direction = Vector2.right;
            FlipModelX(false);
        }
    }

}
