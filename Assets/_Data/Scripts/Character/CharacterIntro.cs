using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterIntro : CharacterAbstract
{
    [Header("Character Intro")]
    [SerializeField] protected SpriteRenderer sprite;
    public SpriteRenderer Sprite => sprite;
    [SerializeField] protected AnimationClip transformClip;
    [SerializeField] protected bool isReady = false;
    public bool IsReady => isReady;
    [SerializeField] private bool isIntroPlaying = false;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSprite();
    }
    protected void LoadSprite()
    {
        if (this.sprite != null) return;
        this.sprite = GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadSprite");
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

        this.characterCtrl.Animator.Play(transformClip.name);
        yield return new WaitForSeconds(transformClip.length + 0.5f);

        this.characterCtrl.StateManager.SetNextState(new IdleState());
        yield return new WaitForSeconds(1f);

        this.isReady = true;
    }

}
