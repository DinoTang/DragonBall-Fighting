using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCrouchHitState : State
{
    public float duration = 0.5f;
    protected int attackCounter;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        this.animator.SetBool("IsCrouch", true);
        this.animator.SetBool("IsAttack", true);
        this.animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!this.stateManager.CharacterCtrl.CharacterIntro.IsReady) return;
        if (InputManager.Instance.ReleaseCrouchInput())
        {
            this.animator.SetBool("IsCrouch", false);
            this.stateManager.SetNextState(new IdleCombatState());
        }
        if (Fixedtime >= duration)
        {
            this.BackMainState();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsAttack", false);
    }
    protected void BackMainState()
    {
        this.stateManager.SetNextState(new CrouchState());
    }
}
