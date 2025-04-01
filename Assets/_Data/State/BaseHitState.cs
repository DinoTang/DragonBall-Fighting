using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitState : State
{
    public float duration = 0.5f;
    protected bool shouldCombo = false;
    protected int attackCounter;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", true);
        this.stateManager.CharacterCtrl.Animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!this.stateManager.CharacterCtrl.CharacterIntro.IsReady) return;
        if (Fixedtime > duration * 0.5f && InputManager.Instance.GetNormalHitInput())
        {
            shouldCombo = true;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", false);
    }
    protected void BackMainState()
    {
        this.stateManager.SetNextStateToMain();
    }
}
