using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class DamageReceiver : BaseBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CapsuleCollider2D collide;
    [SerializeField] protected bool isImmortal = false;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isHurt = false;
    public bool IsHurt => isHurt;
    [SerializeField] protected bool isBlock = false;
    public bool IsBlock => isBlock;
    [SerializeField] protected bool isKnockDown = false;
    public bool IsKnockDown => isKnockDown;
    [SerializeField] protected bool isKnockUp = false;
    public bool IsKnockUp => isKnockUp;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP = 10000;
    public void SetIsHurt(bool isHurt)
    {
        this.isHurt = isHurt;
    }
    public void SetIsBlock(bool isBlock)
    {
        this.isBlock = isBlock;
    }
    public void SetIsImmortal(bool isImmortal)
    {
        this.isImmortal = isImmortal;
    }
    public void SetIsKnockDown(bool isKnockDown)
    {
        this.isKnockDown = isKnockDown;
    }
    public void SetIsKnockUp(bool isKnockUp)
    {
        this.isKnockUp = isKnockUp;
    }
    protected override void OnEnable()
    {
        this.isDead = false;
        base.OnEnable();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
        this.currentHP = this.maxHP;
    }

    protected void LoadCollider()
    {
        if (this.collide != null) return;
        this.collide = GetComponent<CapsuleCollider2D>();
        Debug.Log(transform.name + ": LoadCollider");
    }
    protected void AddHP(int hp)
    {
        if (this.isDead) return;
        if (this.currentHP >= this.maxHP) return;
        this.currentHP += hp;
    }

    public virtual bool DeductHP(float hp)
    {
        if (this.isImmortal || this.isDead || this.isBlock) return false;
        this.OnHurt();

        this.currentHP -= hp;
        this.IsDead();
        return true;
    }

    public bool CheckDead()
    {
        return this.currentHP <= 0;
    }
    protected void IsDead()
    {
        if (!this.CheckDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected virtual void OnDead()
    {

    }
    public abstract bool OnBlock();

    protected virtual void OnHurt()
    {
        this.isHurt = true;

    }
}
