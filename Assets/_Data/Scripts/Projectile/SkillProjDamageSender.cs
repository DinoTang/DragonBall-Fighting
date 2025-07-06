using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class SkillProjDamageSender : DamageSender
{
    [Header("Skill Projectile Damage Sender")]
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
        this.damage = this.skillProjCtrl.SkillData.damage;
    }
    protected void LoadSkillProjCtrl()
    {
        if (this.skillProjCtrl != null) return;
        this.skillProjCtrl = GetComponentInParent<SkillProjCtrl>();
        Debug.Log(transform.name + ": LoadSkillProjCtrl", gameObject);
    }
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is CircleCollider2D circleCollider)
        {
            circleCollider.offset = new Vector2(0.4f, 0);
            circleCollider.radius = 1.5f;
        }

    }
    protected override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl target = damageReceiver.transform.parent.GetComponent<CharacterCtrl>();
        if (this.skillProjCtrl.SkillProj.Owner == target) return;
        base.SendDamage(damageReceiver);
        this.skillProjCtrl.SkillProjDespawn.DoDespawn();
    }

    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        string explosionFXName = this.skillProjCtrl.SkillData.damageFxName.ToString();
        this.SpawnHitExplosionFX(explosionFXName, target);
    }
}