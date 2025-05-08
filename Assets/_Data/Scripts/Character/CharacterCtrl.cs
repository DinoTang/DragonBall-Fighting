using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class CharacterCtrl : DinoBehaviour
{
    [SerializeField] protected CharacterEnum characterName;
    public CharacterEnum CharacterName => characterName;
    [SerializeField] protected Rigidbody2D rgb;
    public Rigidbody2D Rgb => rgb;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected StateManager stateManager;
    public StateManager StateManager => stateManager;
    [SerializeField] protected CharacterData characterData;
    public CharacterData CharacterData => characterData;
    [SerializeField] protected CharacterIntro characterIntro;
    public CharacterIntro CharacterIntro => characterIntro;
    [SerializeField] protected CharacterFlip characterFlip;
    public CharacterFlip CharacterFlip => characterFlip;
    [SerializeField] protected CharacterDamageSender characterDamageSender;
    public CharacterDamageSender CharacterDamageSender => characterDamageSender;
    [SerializeField] protected CharacterDamageReceiver characterDamageReceiver;
    public CharacterDamageReceiver CharacterDamageReceiver => characterDamageReceiver;
    [SerializeField] protected KiCharge kiCharge;
    [SerializeField] protected FirePoint firePoint;
    public FirePoint FirePoint => firePoint;
    public bool IsGround { get; set; }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadAnimator();
        this.LoadStateManager();
        this.LoadCharacterData();
        this.LoadCharacterIntro();
        this.LoadCharacterFlip();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadKiCharge();
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
    protected void LoadCharacterData()
    {
        if (this.characterData != null) return;
        this.characterData = GetComponentInChildren<CharacterData>();
        Debug.Log(transform.name + ": LoadCharacterData", gameObject);
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
        if (this.characterDamageSender != null) return;
        this.characterDamageSender = GetComponentInChildren<CharacterDamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }
    protected void LoadDamageReceiver()
    {
        if (this.characterDamageReceiver != null) return;
        this.characterDamageReceiver = GetComponentInChildren<CharacterDamageReceiver>();
        Debug.Log(transform.name + ": LoadDamageReceiver", gameObject);
    }
    protected void LoadKiCharge()
    {
        if (this.kiCharge != null) return;
        this.kiCharge = GetComponentInChildren<KiCharge>();
        Debug.Log(transform.name + ": LoadKiCharge", gameObject);

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
        if (this.stateManager.CurrentState.GetType() == typeof(IdleCombatState)) return;
        this.stateManager.SetNextState(new IdleCombatState());
        this.stateManager.CharacterCtrl.Animator.SetBool("IsJump", false);
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, transform.position);
    }

    public void EnableSmokeEffect()
    {
        this.kiCharge.gameObject.SetActive(true);
    }
    public void DisableSmokeEffect()
    {
        this.kiCharge.gameObject.SetActive(false);
    }

}

