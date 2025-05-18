using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class BasicProjDamageSender : DamageSender
{
    [Header("Basic Projectile Damage Sender")]
    [SerializeField] protected BasicProjCtrl basicProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBasicProjCtrl();
    }
    protected void LoadBasicProjCtrl()
    {
        if (this.basicProjCtrl != null) return;
        this.basicProjCtrl = GetComponentInParent<BasicProjCtrl>();
        Debug.Log(transform.name + ": LoadBasicProjCtrl", gameObject);
    }
    protected override void LoadCollider()
    {
        base.LoadCollider();
        this._collider.offset = new Vector2(0.4f, 0);
        this._collider.radius = 0.3f;
    }
    protected override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl owner = damageReceiver.transform.parent.GetComponent<CharacterCtrl>();
        if (this.basicProjCtrl.BasicProj.Owner == owner) return;
        base.SendDamage(damageReceiver);
        this.basicProjCtrl.BasicProjDespawn.DoDespawn();
    }
    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        this.SpawnHitExplosionFX(ExplosionFXSpawner.efx_DefaultName, target);
    }
}