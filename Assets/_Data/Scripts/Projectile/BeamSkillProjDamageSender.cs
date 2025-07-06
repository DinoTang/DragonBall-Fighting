using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BeamSkillProjDamageSender : DamageSender
{
    [Header("Beam Skill Projectile Damage Sender")]
    [SerializeField] protected SkillProjCtrl skillProjCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSkillProjCtrl();
    }
    protected override void LoadCollider()
    {
        base.LoadCollider();
        if (this._collider is BoxCollider2D boxCollider)
        {
            boxCollider.size = new Vector2(4.3f, 1.7f);
        }
    }
    protected void LoadSkillProjCtrl()
    {
        if (this.skillProjCtrl != null) return;
        this.skillProjCtrl = GetComponentInParent<SkillProjCtrl>();
        Debug.Log(transform.name + ": LoadSkillProjCtrl", gameObject);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;

        // Gây damage theo tick
        if (!isHitting)
            StartCoroutine(DamageTick(damageReceiver));
    }

    private IEnumerator DamageTick(DamageReceiver target)
    {
        isHitting = true;
        while (_collider.enabled && target != null)
        {
            target.DeductHP(this.damage);
            this.OnHitExplosionFX(target.transform);
            yield return new WaitForSeconds(0.2f);
        }
        isHitting = false;
    }
    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        string explosionFXName = this.skillProjCtrl.SkillData.damageFxName.ToString();
        ExplosionFX efx = this.SpawnHitExplosionFX(explosionFXName, target);
        efx.transform.position = new Vector2(efx.transform.position.x, efx.transform.position.y - 0.5f);
    }
}