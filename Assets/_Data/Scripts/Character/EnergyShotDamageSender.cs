using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
public class EnergyShotDamageSender : DamageSender
{
    [Header("Energy Shot Damage Sender")]
    [SerializeField] protected EnergyShotCtrl energyShotCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnergyShotCtrl();
    }
    protected void LoadEnergyShotCtrl()
    {
        if (this.energyShotCtrl != null) return;
        this.energyShotCtrl = GetComponentInParent<EnergyShotCtrl>();
        Debug.Log(transform.name + ": LoadEnergyShotCtrl", gameObject);
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
        if (this.energyShotCtrl.EnergyShot.Owner == owner) return;
        base.SendDamage(damageReceiver);
        this.energyShotCtrl.EnergyShotDespawn.DoDespawn();
        ExplosionFX explosionFX = ExplosionFXSpawner.Instance.Spawn(ExplosionFXSpawner.Instance.explosionFXSpawner, damageReceiver.transform.position);
    }
}