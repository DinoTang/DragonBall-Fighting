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
        this.OnUpdate();
    }
    protected virtual void OnUpdate()
    {
        this.Move();
    }
    // Tách phần di chuyển
    protected virtual void Move()
    {
        this.transform.Translate(this.speed * Time.deltaTime * this.direction);
    }

    public override void Init(CharacterCtrl owner)
    {
        this.owner = owner;

        if (!owner.CharacterFlip.IsFacingRight)
        {
            this.SettingDirect(-1, true);
        }
        else
        {
            this.SettingDirect(1, false);
        }
    }


    protected virtual void SettingDirect(int side, bool boolean)
    {
        this.direction = Vector2.right * side;
        this.FlipModelX(boolean);
    }
}
