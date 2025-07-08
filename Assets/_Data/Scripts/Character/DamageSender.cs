using System.Collections;
using UnityEngine;

public class DamageSender : DinoBehaviour
{
    [Header("Damage Sender")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected bool isHitting = false;
    public bool IsHitting => isHitting;
    public void SetIsHitting(bool isHitting)
    {
        this.isHitting = isHitting;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.isHitting = false;
    }
    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<Collider2D>();
        this._collider.isTrigger = true;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    public void EnableCollider()
    {
        // Debug.Log("Collider enabled at: " + Time.time);
        this._collider.enabled = true;
    }

    public void DisableCollider()
    {
        // Debug.Log("Collider disable at: " + Time.time);
        this._collider.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.SendDamage(damageReceiver);
        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);
    }

    protected virtual void SendDamage(DamageReceiver damageReceiver)
    {
        this.isHitting = true;
        if (damageReceiver.OnBlock()) return;
        damageReceiver.DeductHP(this.damage);
        this.OnHitExplosionFX(damageReceiver.transform);
    }
    protected virtual void OnHitExplosionFX(Transform target)
    {

    }
    protected ExplosionFX SpawnHitExplosionFX(ExplosionFXEnum efx_name, Transform target, CharacterCtrl owner = null)
    {
        ExplosionFX efx = ExplosionFXSpawner.Instance.Spawn(efx_name.ToString(), target.position, owner);
        return efx;
    }



}