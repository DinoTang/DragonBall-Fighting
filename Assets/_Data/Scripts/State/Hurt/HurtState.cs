using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected float hurtDuration = 0.5f;
    protected Vector2 knockbackForce;
    protected int hurtCounter;
    protected bool shouldHurt = false;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsHurt(false);
        this.animator.SetBool("IsHurt", true);
        this.animator.SetInteger("HurtCounter", hurtCounter);
        Debug.Log("Player Hurt: " + hurtCounter);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.stateManager.CharacterCtrl.CharacterDamageReceiver.IsHurt) this.shouldHurt = true;
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
    }
}
