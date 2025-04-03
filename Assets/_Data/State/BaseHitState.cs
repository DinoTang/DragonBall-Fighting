using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHitState : State
{
    protected float duration = 0.5f;
    protected bool shouldCombo = false;
    protected int attackCounter;
    protected float attackMoveSpeed = 1f;
    protected float attackMoveDuration = 0.1f;
    protected float attackMoveTime;
    protected bool isMovingDuringAttack = false;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        this.attackMoveTime = Time + this.attackMoveDuration;
        this.isMovingDuringAttack = true;
        this.animator.SetBool("IsAttack", true);
        this.animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.MoveShortDistance();
        if (Fixedtime > duration * 0.5f && InputManager.Instance.GetNormalHitInput())
        {
            shouldCombo = true;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsAttack", false);
    }
    protected void BackMainState()
    {
        this.stateManager.SetNextStateToMain();
    }
    protected void MoveShortDistance()
    {
        if (isMovingDuringAttack && Time < attackMoveTime)
        {
            float moveStep = this.attackMoveSpeed * this.Time * this.stateManager.transform.localScale.x;
            this.stateManager.CharacterCtrl.transform.position += new Vector3(moveStep, 0, 0);
        }
        else
        {
            this.isMovingDuringAttack = false;
        }
    }
}
