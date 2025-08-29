using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class BlastSkillDamageSender : BaseSkillDamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider)
        {
            circleCollider.offset = new Vector2(0.4f, 0);
            circleCollider.radius = 0.3f;
        }
    }

    public override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl opponent = damageReceiver.transform.GetComponentInParent<CharacterCtrl>();
        CharacterCtrl owner = this.skillProjCtrl.Owner;
        if (owner == opponent) return;
        if (opponent.StateManager.CurrentState is KnockdownIdleState) return;
        base.SendDamage(damageReceiver);
        this.skillProjCtrl.SkillProjDespawn.DoDespawn();
    }

    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        this.SpawnHitExplosionFX(this.skillProjCtrl.SkillData.damageFxName, target);
    }
}