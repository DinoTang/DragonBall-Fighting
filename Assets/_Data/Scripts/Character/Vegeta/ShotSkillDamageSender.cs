using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class ShotSkillDamageSender : DamageSender
{
    [Header("Shot Skill Damage Sender")]
    [SerializeField] protected ShotSkillCtrl shotSkillCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadShotSkillCtrl();
        this.damage = this.shotSkillCtrl.ShotSkillData.damage;
    }
    protected void LoadShotSkillCtrl()
    {
        if (this.shotSkillCtrl != null) return;
        this.shotSkillCtrl = GetComponentInParent<ShotSkillCtrl>();
        Debug.Log(transform.name + ": LoadShotSkillCtrl", gameObject);
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
        if (this.shotSkillCtrl.ShotSkill.Owner == owner) return;
        base.SendDamage(damageReceiver);
        this.shotSkillCtrl.ShotSkillDespawn.DoDespawn();
    }

    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        string explosionFXName = this.shotSkillCtrl.ShotSkillData.fxName;
        this.SpawnHitExplosionFX(explosionFXName, target);
    }
}