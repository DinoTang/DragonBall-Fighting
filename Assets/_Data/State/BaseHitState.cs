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
        this.stateManager.CharacterCtrl.CharacterAttack.SetIsAttack(true);
        //Attack
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", this.stateManager.CharacterCtrl.CharacterAttack.IsAttack);
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

    protected void BackMainState()
    {
        this.stateManager.CharacterCtrl.CharacterAttack.SetIsAttack(false);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", this.stateManager.CharacterCtrl.CharacterAttack.IsAttack);
        this.stateManager.SetNextStateToMain();
    }
}
