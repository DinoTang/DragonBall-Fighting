using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected float hurtDuration = 0.5f;
    protected Vector2 knockbackForce;
    protected int hurtCounter;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsHurt", true);
        this.animator.SetInteger("HurtCounter", hurtCounter);
        Debug.Log("Player Hurt: " + hurtCounter);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Fixedtime >= this.hurtDuration)
        {
            // this.stateManager.SetNextState(new IdleCombatState());
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsHurt", false);
    }
}
