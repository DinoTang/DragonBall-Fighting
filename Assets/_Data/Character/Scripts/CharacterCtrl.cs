using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class CharacterCtrl : DinoBehaviour
{
    [SerializeField] protected Rigidbody2D rgb;
    public Rigidbody2D Rgb => rgb;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected StateManager stateManager;
    public StateManager StateManager => stateManager;
    [SerializeField] protected CharacterIntro characterIntro;
    public CharacterIntro CharacterIntro => characterIntro;
    [SerializeField] protected CharacterFlip characterFlip;
    public CharacterFlip CharacterFlip => characterFlip;
    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender => damageSender;
    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;
    [SerializeField] protected EnergyShotSpawner energyShotSpawner;
    public EnergyShotSpawner EnergyShotSpawner => energyShotSpawner;
    [SerializeField] protected EnergyShot energyShot;
    public EnergyShot EnergyShot => energyShot;
    [SerializeField] protected EnergyShotFXSpawner energyShotFXSpawner;
    public EnergyShotFXSpawner EnergyShotFXSpawner => energyShotFXSpawner;
    [SerializeField] protected EnergyShotFX energyShotFX;
    public EnergyShotFX EnergyShotFX => energyShotFX;
    [SerializeField] protected FirePoint firePoint;
    public FirePoint FirePoint => firePoint;
    public bool IsGround { get; set; }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadAnimator();
        this.LoadStateManager();
        this.LoadCharacterIntro();
        this.LoadCharacterFlip();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadEnergyShotSpawner();
        this.LoadEnergyShot();
        this.LoadFXSpawner();
        this.LoadEnergyShotFX();
        this.LoadFirePoint();
    }
    protected void LoadRigidbody()
    {
        if (this.rgb != null) return;
        this.rgb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
    protected void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
    protected void LoadStateManager()
    {
        if (this.stateManager != null) return;
        this.stateManager = GetComponent<StateManager>();
        Debug.Log(transform.name + ": LoadStateManager", gameObject);
    }
    protected void LoadCharacterIntro()
    {
        if (this.characterIntro != null) return;
        this.characterIntro = GetComponentInChildren<CharacterIntro>();
        Debug.Log(transform.name + ": LoadCharacterIntro", gameObject);
    }
    protected void LoadCharacterFlip()
    {
        if (this.characterFlip != null) return;
        this.characterFlip = GetComponentInChildren<CharacterFlip>();
        Debug.Log(transform.name + ": LoadCharacterFlip", gameObject);
    }
    protected void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }
    protected void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = GetComponentInChildren<DamageReceiver>();
        Debug.Log(transform.name + ": LoadDamageReceiver", gameObject);
    }
    protected void LoadEnergyShotSpawner()
    {
        if (this.energyShotSpawner != null) return;
        this.energyShotSpawner = FindAnyObjectByType<EnergyShotSpawner>();
        Debug.Log(transform.name + ": LoadEnergyShotSpawner", gameObject);
    }
    protected void LoadEnergyShot()
    {
        if (this.energyShot != null) return;
        this.energyShot = FindAnyObjectByType<EnergyShot>();
        Debug.Log(transform.name + ": LoadEnergyShot", gameObject);
    }
    protected void LoadFXSpawner()
    {
        if (this.energyShotFXSpawner != null) return;
        this.energyShotFXSpawner = FindAnyObjectByType<EnergyShotFXSpawner>();
        Debug.Log(transform.name + ": LoadFXSpawner", gameObject);
    }
    protected void LoadEnergyShotFX()
    {
        if (this.energyShotFX != null) return;
        this.energyShotFX = FindAnyObjectByType<EnergyShotFX>();
        Debug.Log(transform.name + ": LoadEnergyShotFX", gameObject);
    }
    protected void LoadFirePoint()
    {
        if (this.firePoint != null) return;
        this.firePoint = FindAnyObjectByType<FirePoint>();
        Debug.Log(transform.name + ": loadFirePoint", gameObject);

    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Ground groundCollider = collision.transform.GetComponent<Ground>();
        if (groundCollider == null) return;
        if (this.stateManager.CurrentState == null) return;
        this.IsGround = true;
        // if (this.stateManager.CurrentState.GetType() == typeof(IdleCombatState)) return;
        // this.stateManager.SetNextState(new IdleCombatState());
        // this.stateManager.CharacterCtrl.Animator.SetBool("IsJump", false);
    }
}
