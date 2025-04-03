using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public class DamageReceiver : DinoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CapsuleCollider2D collide;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP = 5;
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

    public void DeductHP(float hp)
    {
        if (this.isDead) return;
        this.currentHP -= hp;
        this.IsDead();
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
}
