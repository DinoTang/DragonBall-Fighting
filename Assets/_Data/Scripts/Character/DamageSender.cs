using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class DamageSender : DinoBehaviour
{
    [SerializeField] protected CircleCollider2D _collider;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected bool isHitSuccessful = false;
    public bool IsHitSuccessful => isHitSuccessful;
    public void SetIsHitSuccessful(bool isHitSuccessful)
    {
        this.isHitSuccessful = isHitSuccessful;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<CircleCollider2D>();
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
        damageReceiver.DeductHP(this.damage);
        this.isHitSuccessful = true;
    }
}