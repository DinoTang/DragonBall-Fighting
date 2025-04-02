using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirHitState : BaseAirState
{
    public float duration = 0.5f;
    protected int attackCounter;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        this.stateManager.CharacterCtrl.Animator.SetBool("IsJump", true);
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", true);
        this.stateManager.CharacterCtrl.Animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Fixedtime >= duration)
        {
            this.BackMainState();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.stateManager.CharacterCtrl.Animator.SetBool("IsAttack", false);
    }
    

}
