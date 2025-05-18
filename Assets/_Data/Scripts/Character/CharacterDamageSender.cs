using System.Collections;
using UnityEngine;
public class CharacterDamageSender : DamageSender
{
    [Header("Character Damage Sender")]
    [SerializeField] CharacterCtrl characterCtrl;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerCtrl();
    }
    protected void LoadPlayerCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }
    protected override void LoadCollider()
    {
        base.LoadCollider();
        this._collider.enabled = false;
    }
    protected override void OnHitExplosionFX(Transform target)
    {
        base.OnHitExplosionFX(target);
        this.SpawnHitExplosionFX("Hit_Effect", target, this.characterCtrl);
    }
}