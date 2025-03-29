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
    [SerializeField] protected CharacterMovement characterMovement;
    public CharacterMovement CharacterMovement => characterMovement;
    [SerializeField] protected CharacterAttack characterAttack;
    public CharacterAttack CharacterAttack => characterAttack;
    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender => damageSender;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody();
        this.LoadAnimator();
        this.LoadStateManager();
        this.LoadCharacterIntro();
        this.LoadCharacterMovement();
        this.LoadCharacterAttack();
        this.LoadDamageSender();
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
    protected void LoadCharacterMovement()
    {
        if (this.characterMovement != null) return;
        this.characterMovement = GetComponentInChildren<CharacterMovement>();
        Debug.Log(transform.name + ": LoadCharacterMovement", gameObject);
    }
    protected void LoadCharacterIntro()
    {
        if (this.characterIntro != null) return;
        this.characterIntro = GetComponentInChildren<CharacterIntro>();
        Debug.Log(transform.name + ": LoadCharacterIntro", gameObject);
    }
    protected void LoadCharacterAttack()
    {
        if (this.characterAttack != null) return;
        this.characterAttack = GetComponentInChildren<CharacterAttack>();
        Debug.Log(transform.name + ": LoadCharacterAttack", gameObject);
    }
    protected void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }
}
