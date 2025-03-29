using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : State
{
    protected float cooldown = 0.5f;

    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.StartCoroutine(this.CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(this.cooldown);
        this.stateManager.SetNextStateToMain();
    }
}
