using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterIntro : CharacterAbstract
{
    public Animator animator;
    public AnimatorController normalController;
    public AnimatorController ssj2Controller;
    public AnimationClip transformClip;
    [SerializeField] protected bool isReady;
    public bool IsReady => isReady;
    protected override void Start()
    {
        // animator.runtimeAnimatorController = normalController;
        // StartCoroutine(TransformToSSJ2());
        this.characterCtrl.StateManager.SetNextState(new IdleCombatState());
        this.characterCtrl.IsGround = true;
        this.isReady = true;
    }
    IEnumerator TransformToSSJ2()
    {
        yield return new WaitForSeconds(1f);

        animator.Play(transformClip.name);
        yield return new WaitForSeconds(transformClip.length);

        this.characterCtrl.StateManager.SetNextState(new IdleState());
        yield return new WaitForSeconds(2f);

        animator.runtimeAnimatorController = ssj2Controller;
        this.characterCtrl.StateManager.SetNextState(new IdleCombatState());
        this.characterCtrl.IsGround = true;
        this.isReady = true;
    }

    protected void EnableHitbox()
    {
        this.characterCtrl.DamageSender.EnableCollider();
    }

    protected void DisableHitbox()
    {
        this.characterCtrl.DamageSender.DisableCollider();
    }

    protected void EnableSpawnEnergyShot()
    {
        Vector2 newPos = this.characterCtrl.transform.position;
        newPos.x += 1.5f;
        Quaternion newRot = this.characterCtrl.transform.rotation;
        this.characterCtrl.EnergyShotSpawner.Spawn(this.characterCtrl.EnergyShot, newPos, newRot);
    }

    protected void EnableSpawnShotEffect()
    {
        Vector2 newPos = this.characterCtrl.transform.position;
        newPos.x += 1.5f;
        Quaternion newRot = this.characterCtrl.transform.rotation;
        this.characterCtrl.FXSpawner.Spawn(this.characterCtrl.EnergyShotFX, newPos, newRot);
    }
}
