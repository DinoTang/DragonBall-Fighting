using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterIntro : CharacterAbstract
{
    [Header("Character Intro")]
    public AnimationClip transformClip;
    [SerializeField] protected bool isReady = false;
    public bool IsReady => isReady;
    [SerializeField] private bool isIntroPlaying = false;


    public void StartIntroCharacter()
    {
        if (isIntroPlaying) return; // tránh gọi nhiều lần
        isIntroPlaying = true;
        StartCoroutine(this.StartIntroSequence());
    }
    IEnumerator StartIntroSequence()
    {
        this.characterCtrl.StateManager.SetNextState(new IntroState());
        CameraManager.Instance.Focus(this.transform.position, 3f);
        yield return new WaitForSeconds(1f);

        this.characterCtrl.Animator.SetInteger("PoseCounter", 3);
        yield return new WaitForSeconds(transformClip.length + 0.5f);

        this.characterCtrl.StateManager.SetNextState(new IdleCombatState());
        yield return new WaitForSeconds(1f);

        this.isReady = true;
    }

}
