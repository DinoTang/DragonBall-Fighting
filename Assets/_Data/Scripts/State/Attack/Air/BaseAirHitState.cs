using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirHitState : BaseAirState
{
    protected int attackCounter;
    protected bool shouldCombo = false;
    protected bool isHitting;
    protected bool otherAttack;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        // this.duration = 0.5f;

        //Attack
        this.animator.SetBool("IsJump", true);
        this.animator.SetBool("IsAttack", true);
        this.animator.SetInteger("AttackCounter", attackCounter);
        Debug.Log("Player Attack " + attackCounter + " Fired!");

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.isHitting = this.stateManager.CharacterCtrl.CharacterDamageSender.IsHitting;
        if (this.isHitting)
        {
            this.stateManager.CharacterCtrl.Rgb.velocity = Vector2.zero;
            this.stateManager.CharacterCtrl.Rgb.gravityScale = 0;
        }
        if (this.timed > duration * 0.3f && InputManager.Instance.GetHitInput())
        {
            shouldCombo = true;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsAttack", false);
        this.stateManager.CharacterCtrl.Rgb.gravityScale = 15;
    }


}
