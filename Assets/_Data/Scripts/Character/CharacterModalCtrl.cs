using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CharacterModalCtrl : CharacterAbstract
{
    [Header("Character Modal Ctrl")]
    [SerializeField] protected SpriteRenderer sprite;
    public SpriteRenderer Sprite => sprite;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AnimationClip readyClip;
    [SerializeField] protected bool isReady = false;
    public bool IsReady => isReady;
    [SerializeField] private bool isIntroPlaying = false;
    public void LoadCharacterModalCtrlComponent()
    {
        this.LoadComponent();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
        this.LoadData();
    }

    protected void LoadSpriteRenderer()
    {
        if (this.sprite != null) return;
        this.sprite = GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadSpriteRenderer");
    }

    protected void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator");
    }

    protected void LoadData()
    {
        this.sprite.sprite = this.characterCtrl.CharacterDataSO.portrait;
        this.animator.runtimeAnimatorController = this.characterCtrl.CharacterDataSO.animator;
        this.readyClip = this.characterCtrl.CharacterDataSO.readyClip;
        Debug.Log(transform.name + ": LoadData");
    }

    public void StartIntroCharacter(float waitTime)
    {
        if (isIntroPlaying) return; // tránh gọi nhiều lần
        isIntroPlaying = true;
        StartCoroutine(this.StartIntroSequence(waitTime));
    }
    IEnumerator StartIntroSequence(float waitTime)
    {
        this.characterCtrl.StateManager.SetNextState(new IntroState());
        CameraManager.Instance.Focus(this.transform.position, 3f);
        yield return new WaitForSeconds(waitTime);

        this.characterCtrl.Animator.Play(readyClip.name);
        yield return new WaitForSeconds(readyClip.length + 0.5f);

        this.characterCtrl.StateManager.SetNextState(new IdleState());
        yield return new WaitForSeconds(1f);

        this.isReady = true;
    }
}
