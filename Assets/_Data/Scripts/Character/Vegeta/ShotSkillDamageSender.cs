using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class ShotSkillDamageSender : DamageSender
{
    [Header("Shot Skill Damage Sender")]
    [SerializeField] protected VegetaShotSkillCtrl vegetaShotSkillCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadVegetaShotSkillCtrl();
    }
    protected void LoadVegetaShotSkillCtrl()
    {
        if (this.vegetaShotSkillCtrl != null) return;
        this.vegetaShotSkillCtrl = GetComponentInParent<VegetaShotSkillCtrl>();
        Debug.Log(transform.name + ": LoadVegetaShotSkillCtrl", gameObject);
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
        if (this.vegetaShotSkillCtrl.VegetaShotSkill.Owner == owner) return;
        base.SendDamage(damageReceiver);
        this.vegetaShotSkillCtrl.VegetaShotSkillDespawn.DoDespawn();
        ExplosionFX explosionFX = ExplosionFXSpawner.Instance.Spawn(ExplosionFXSpawner.Instance.explosionFXSpawner, damageReceiver.transform.position);
    }
    protected virtual void SpawnExplosionFX()
    {
        
    }
}