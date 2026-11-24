using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class DamageReceiver : BaseBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CapsuleCollider2D collide;
    [SerializeField] protected bool isImmortal = false;
    [SerializeField] public bool isDead = false;
    [SerializeField] protected bool isHurt = false;
    public bool IsHurt => isHurt;
    [SerializeField] protected bool isBlock = false;
    public bool IsBlock => isBlock;
    [SerializeField] protected bool isKnockDown = false;
    public bool IsKnockDown => isKnockDown;
    [SerializeField] protected bool isKnockUp = false;
    public bool IsKnockUp => isKnockUp;
    [SerializeField] protected float currentHP;
    public float CurrentHP => currentHP;
    [SerializeField] protected float hpMax = 10000;
    public float HpMax => hpMax;
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
    public void ResetHP()
    {
        this.currentHP = this.hpMax;
        this.isDead = false;
        this.isHurt = false;
        this.isKnockDown = false;
        this.isKnockUp = false;
    }
    public void SetCurrentHP(float hp)
    {
        this.currentHP = Mathf.Clamp(hp, 0, this.hpMax);
        this.isDead = this.currentHP <= 0;
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
        this.currentHP = this.hpMax;
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
        if (this.currentHP >= this.hpMax) return;
        this.currentHP += hp;
    }

    public virtual bool DeductHP(float hp)
    {
        if (!this.CanReceiveDamage()) return false;
        if (this.TryHandleDamage()) return false;
        this.TakeDamage(hp);
        return true;
    }
    protected virtual bool CanReceiveDamage()
    {
        return !isDead && !isImmortal;
    }
    protected virtual bool TryHandleDamage()
    {
        return false; // MẶC ĐỊNH: KHÔNG XỬ LÝ
    }
    protected void TakeDamage(float damage)
    {
        this.OnHurt();
        this.ApplyDamage(damage);
        this.IsDead();
    }
    protected void ApplyDamage(float damage)
    {
        this.currentHP -= damage;
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

    protected virtual void OnHurt()
    {
        this.isHurt = true;

    }
}
