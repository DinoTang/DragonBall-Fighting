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
    [SerializeField] protected CharacterGroundCollision characterGroundCollision;
    public CharacterGroundCollision CharacterGroundCollision => characterGroundCollision;
    // [SerializeField] protected CharacterData characterData;
    // public CharacterData CharacterData => characterData;
    [SerializeField] protected CharacterIntro characterIntro;
    public CharacterIntro CharacterIntro => characterIntro;
    [SerializeField] protected CharacterFlip characterFlip;
    public CharacterFlip CharacterFlip => characterFlip;
    [SerializeField] protected CharacterAnimEvent characterAnimEvent;
    public CharacterAnimEvent CharacterAnimEvent => characterAnimEvent;
    [SerializeField] protected CharacterDamageSender characterDamageSender;
    public CharacterDamageSender CharacterDamageSender => characterDamageSender;
    [SerializeField] protected CharacterDamageReceiver characterDamageReceiver;
    public CharacterDamageReceiver CharacterDamageReceiver => characterDamageReceiver;
    [SerializeField] protected CharacterCombo characterCombo;
    public CharacterCombo CharacterCombo => characterCombo;
    [SerializeField] protected KiCharge kiCharge;
    [SerializeField] protected FirePoint firePoint;
    public FirePoint FirePoint => firePoint;

    [Header("Ki System")]
    [SerializeField] protected CharacterKiManager characterKiManager;
    public CharacterKiManager CharacterKiManager => characterKiManager;
    [SerializeField] protected CharacterSkillManager characterSkillManager;
    public CharacterSkillManager CharacterSkillManager => characterSkillManager;

    [Header("Opponent")]
    [SerializeField] protected CharacterCtrl opponent;
    public CharacterCtrl Opponent => opponent;
    public void SetOpponent(CharacterCtrl target)
    {
        opponent = target;
    }


    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadAnimator();
        this.LoadStateManager();
        this.LoadCharacterGroundCollision();
        // this.LoadCharacterData();
        this.LoadCharacterIntro();
        this.LoadCharacterFlip();
        this.LoadCharacterAnimEvent();
        this.LoadCharacterCombo();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadKiCharge();
        this.LoadFirePoint();
        this.LoadCharacterKiManager();
        this.LoadCharacterSkillManager();
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
    protected void LoadCharacterGroundCollision()
    {
        if (this.characterGroundCollision != null) return;
        this.characterGroundCollision = GetComponentInChildren<CharacterGroundCollision>();
        Debug.Log(transform.name + ": LoadCharacterGroundCollision", gameObject);
    }
    // protected void LoadCharacterData()
    // {
    //     if (this.characterData != null) return;
    //     this.characterData = GetComponentInChildren<CharacterData>();
    //     Debug.Log(transform.name + ": LoadCharacterData", gameObject);
    // }
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
    protected void LoadCharacterAnimEvent()
    {
        if (this.characterAnimEvent != null) return;
        this.characterAnimEvent = GetComponentInChildren<CharacterAnimEvent>();
        Debug.Log(transform.name + ": LoadCharacterAnimEvent", gameObject);
    }
    protected void LoadCharacterCombo()
    {
        if (this.characterCombo != null) return;
        this.characterCombo = GetComponentInChildren<CharacterCombo>();
        Debug.Log(transform.name + ": LoadCharacterCombo", gameObject);
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
    protected void LoadCharacterKiManager()
    {
        if (this.characterKiManager != null) return;
        this.characterKiManager = GetComponentInChildren<CharacterKiManager>();
        Debug.Log(transform.name + ": LoadCharacterKiManager", gameObject);

    }
    protected void LoadCharacterSkillManager()
    {
        if (this.characterSkillManager != null) return;
        this.characterSkillManager = GetComponentInChildren<CharacterSkillManager>();
        Debug.Log(transform.name + ": LoadCharacterSkillManager", gameObject);
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

