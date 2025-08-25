using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGroundAttackState : State
{
    protected bool shouldCombo = false;
    protected bool isHitting;
    protected int attackCounter;
    protected float attackMoveSpeed = 0.1f;
    protected float attackMoveDuration = 0.1f;
    protected float attackMoveTime;
    protected bool isMovingDuringAttack = false;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        //Attack
        this.attackMoveTime = timed + this.attackMoveDuration;
        this.isMovingDuringAttack = true;
        this.animator.SetBool("IsAttack", true);
        this.animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");
        this.stateManager.CharacterCtrl.CharacterDamageSender.EnableCollider();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.isHitting = this.stateManager.CharacterCtrl.CharacterDamageSender.IsHitting;

        this.MoveShortDistance();


    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsAttack", false);
        this.stateManager.CharacterCtrl.CharacterDamageSender.DisableCollider();
    }
    protected void BackMainState()
    {
        this.stateManager.SetNextStateToMain();
    }
    protected void MoveShortDistance()
    {
        if (!this.isHitting) return;
        if (isMovingDuringAttack && timed < attackMoveTime)
        {
            float moveStep = this.attackMoveSpeed * this.timed * this.stateManager.transform.localScale.x;
            this.stateManager.CharacterCtrl.transform.position += new Vector3(moveStep, 0, 0);
        }
        else
        {
            this.isMovingDuringAttack = false;
        }
    }
}
