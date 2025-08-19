using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class DiagonalProjDamageSender : DamageSender
{
    [Header("Basic Projectile Damage Sender")]
    [SerializeField] protected DiagonalProjCtrl diagonalProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBasicProjCtrl();
    }
    protected void LoadBasicProjCtrl()
    {
        if (this.diagonalProjCtrl != null) return;
        this.diagonalProjCtrl = GetComponentInParent<DiagonalProjCtrl>();
        Debug.Log(transform.name + ": LoadBasicProjCtrl", gameObject);
    }
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider)
        {
            circleCollider.offset = new Vector2(0.1f, -0.15f);
            circleCollider.radius = 0.3f;
        }

    }

    public override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl owner = damageReceiver.transform.parent.GetComponent<CharacterCtrl>();
        if (this.diagonalProjCtrl.DiagonalProj.Owner == owner) return;
        base.SendDamage(damageReceiver);
        this.diagonalProjCtrl.DiagonalProjDespawn.DoDespawn();
    }
    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        this.SpawnHitExplosionFX(ExplosionFXEnum.Default, target);
    }
}