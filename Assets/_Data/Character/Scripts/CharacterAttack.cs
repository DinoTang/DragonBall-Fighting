using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterAbstract
{
    [SerializeField] protected bool isAttack;
    public bool IsAttack => isAttack;
    public void SetIsAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
    protected void Update()
    {
        if (!this.characterCtrl.CharacterIntro.IsReady) return;
        this.characterCtrl.Animator.SetBool("IsAttack", this.isAttack);
        if (this.characterCtrl.StateManager.CurrentState.GetType() == typeof(CooldownState)) return;
        if (InputManager.Instance.GetAttackHitInput()
        && this.characterCtrl.StateManager.CurrentState.GetType() == typeof(IdleCombatState))
        {

            this.characterCtrl.StateManager.SetNextState(new GroundHit1State());
        }
    }
}
