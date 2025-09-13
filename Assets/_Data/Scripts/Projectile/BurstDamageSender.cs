using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class BurstDamageSender : BaseSkillDamageSender
{
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider) circleCollider.radius = 3f;
    }

    public override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl opponent = damageReceiver.transform.GetComponentInParent<CharacterCtrl>();
        CharacterCtrl owner = this.skillProjCtrl.Owner;
        if (owner == opponent) return;
        if (opponent.StateManager.CurrentState is KnockdownIdleState) return;
        base.SendDamage(damageReceiver);
        this.skillProjCtrl.SkillProjDespawn.DoDespawn();

        this.KnockBackOpponent();
    }

    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        this.SpawnHitExplosionFX(this.skillProjCtrl.SkillData.explosionFXName, target);
    }

    protected void KnockBackOpponent()
    {
        this.skillProjCtrl.Owner.Opponent.StateManager.SetNextState(new KnockBackState());
    }
}