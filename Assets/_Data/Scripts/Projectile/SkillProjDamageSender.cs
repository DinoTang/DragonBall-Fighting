using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class SkillProjDamageSender : DamageSender
{
    [Header("Skill Projectile Damage Sender")]
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
        this.damage = this.skillProjCtrl.ShotSkillData.damage;
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
        this._collider.offset = new Vector2(0.4f, 0);
        this._collider.radius = 1.5f;
    }
    protected override void SendDamage(DamageReceiver damageReceiver)
    {
        CharacterCtrl owner = damageReceiver.transform.parent.GetComponent<CharacterCtrl>();
        if (this.skillProjCtrl.SkillProj.Owner == owner) return;
        Debug.Log(this.skillProjCtrl.SkillProj.Owner + ", " + owner);
        base.SendDamage(damageReceiver);
        this.skillProjCtrl.SkillProjDespawn.DoDespawn();
    }

    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        string explosionFXName = this.skillProjCtrl.ShotSkillData.fxName;
        this.SpawnHitExplosionFX(explosionFXName, target);
    }
}