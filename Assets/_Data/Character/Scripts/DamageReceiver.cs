using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
public class DamageReceiver : DinoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected CapsuleCollider2D collide;
    [SerializeField] protected bool isImmortal = false;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isHurt = false;
    public bool IsHurt => isHurt;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxHP = 5;
    public void SetIsHurt(bool isHurt)
    {
        this.isHurt = isHurt;
    }
    protected override void OnEnable()
    {
        this.isDead = false;
        base.OnEnable();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterCtrl();
        this.LoadCollider();
        this.currentHP = this.maxHP;
    }
    protected void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterCtrl");
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
        if (this.isImmortal) return;
        if (this.isDead) return;
        this.currentHP -= hp;
        this.OnHurt();
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

    protected virtual void OnHurt()
    {
        this.isHurt = true;
        if (this.characterCtrl.StateManager.CurrentState.GetType() == typeof(Hurt1State) ||
        this.characterCtrl.StateManager.CurrentState.GetType() == typeof(Hurt2State)) return;
        this.characterCtrl.StateManager.SetNextState(new Hurt1State());
    }
}
