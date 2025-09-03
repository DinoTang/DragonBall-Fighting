using System.Collections;
using UnityEngine;
public class CharacterDamageSender : DamageSender
{
    [Header("Character Damage Sender")]
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected int hitCounts;
    public int HitCount => hitCounts;
    [SerializeField] protected float comboResetOriginTime = 1;
    [SerializeField] protected float comboResetTime;
    public float ComboResetTime => comboResetTime;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerCtrl();
    }
    protected void Update()
    {
        this.comboResetTime -= Time.deltaTime;
        if (this.comboResetTime <= 0)
        {
            this.comboResetTime = 0;
            this.hitCounts = 0;
        }
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
        this.SpawnHitExplosionFX(ExplosionFXEnum.Hit_Effect, target, this.characterCtrl);
    }
    public override void SendKnockdownDamage(DamageReceiver damageReceiver)
    {
        base.SendKnockdownDamage(damageReceiver);
        this.characterCtrl.Opponent.StateManager.SetNextState(new KnockDownState());
        this.isKnockingDown = false;
        AudioManager.Instance.AudioSource.PlayOneShot(
                 this.characterCtrl.CharacterData.CharacterDataSO.strongHitClip);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        CharacterCtrl opponent = other.GetComponentInParent<CharacterCtrl>();
        if (this.characterCtrl.Opponent != opponent) return;
        base.OnTriggerEnter2D(other);
        this.hitCounts += 1;
        this.comboResetTime = this.comboResetOriginTime;

    }
    public override void SendDamage(DamageReceiver damageReceiver)
    {
        base.SendDamage(damageReceiver);
        this.characterCtrl.CharacterKiManager.GainKi(2);
        this.DisableCollider();
    }

    protected override void OnSendDamage()
    {
        base.OnSendDamage();
        AudioManager.Instance.AudioSource.PlayOneShot(
               this.characterCtrl.CharacterData.CharacterDataSO.weakHitClip);
    }

    // protected override void OnNoSendDamage()
    // {
    //     base.OnNoSendDamage();
    //     AudioManager.Instance.AudioSource.PlayOneShot(
    //               this.characterCtrl.CharacterData.CharacterDataSO.weakHitClip);
    // }
}