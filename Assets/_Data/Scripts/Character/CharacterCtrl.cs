using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StateManager))]
public class CharacterCtrl : BaseBehaviour
{
    [Header("Note")]
    public string note = "Click reset in GameManager is the best selection";
    [Header("Character")]
    [SerializeField] protected CharacterDataSO characterDataSO;
    public CharacterDataSO CharacterDataSO { get => characterDataSO; set => characterDataSO = value; }
    [SerializeField] protected Rigidbody2D rgb;
    public Rigidbody2D Rgb => rgb;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected StateManager stateManager;
    public StateManager StateManager => stateManager;
    [SerializeField] protected CharacterCollision characterCollision;
    public CharacterCollision CharacterCollision => characterCollision;
    [SerializeField] protected CharacterModalCtrl characterModalCtrl;
    public CharacterModalCtrl CharacterModalCtrl => characterModalCtrl;
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
    [SerializeField] protected FirePoint firePointDiagonal;
    public FirePoint FirePointDiagonal => firePointDiagonal;

    [Header("Ki System")]
    [SerializeField] protected CharacterKiManager characterKiManager;
    public CharacterKiManager CharacterKiManager => characterKiManager;
    [SerializeField] protected CharacterSkillManager characterSkillManager;
    public CharacterSkillManager CharacterSkillManager => characterSkillManager;

    [Header("Opponent")]
    [SerializeField] protected CharacterCtrl opponent;
    public CharacterCtrl Opponent => opponent;

    [Header("Trail")]
    [SerializeField] protected Trail trail;
    public Trail Trail => trail;
    [SerializeField] protected Animator trailAnimator;
    public Animator TrailAnimator => trailAnimator;

    [Header("Player Type")]
    [SerializeField] protected PlayerType playerType;
    public PlayerType PlayerType => playerType;

    public void SetOpponent(CharacterCtrl target)
    {
        opponent = target;
    }
    public void LoadCharacterCtrl()
    {
        this.LoadComponent();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadAnimator();
        this.LoadStateManager();
        this.LoadCharacterGroundCollision();
        this.LoadCharacterModalCtrl();
        this.LoadCharacterFlip();
        this.LoadCharacterAnimEvent();
        this.LoadCharacterCombo();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadKiCharge();
        this.LoadFirePoint();
        this.LoadCharacterKiManager();
        this.LoadCharacterSkillManager();
        this.LoadTrail();
        this.LoadPlayerType();
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
        if (this.characterCollision != null) return;
        this.characterCollision = GetComponentInChildren<CharacterCollision>();
        Debug.Log(transform.name + ": LoadCharacterGroundCollision", gameObject);
    }
    protected void LoadCharacterModalCtrl()
    {
        if (this.characterModalCtrl != null) return;
        this.characterModalCtrl = GetComponentInChildren<CharacterModalCtrl>();
        this.characterModalCtrl.LoadCharacterModalCtrlComponent();
        Debug.Log(transform.name + ": LoadCharacterModalCtrl", gameObject);
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
        // this.kiCharge.gameObject.SetActive(false);
        Debug.Log(transform.name + ": LoadKiCharge", gameObject);

    }
    protected void LoadFirePoint()
    {
        if (this.firePoint != null) return;
        this.firePoint = GetComponentInChildren<FirePoint>();
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
    protected void LoadTrail()
    {
        if (this.trail == null)
        {
            this.trail = FindAnyObjectByType<Trail>();
        }
        if (this.trail != null && this.trailAnimator == null)
        {
            this.trailAnimator = this.trail.GetComponentInChildren<Animator>();
        }
    }

    protected void LoadPlayerType()
    {
        if (this.playerType != null) return;
        this.playerType = GetComponentInChildren<PlayerType>();
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

