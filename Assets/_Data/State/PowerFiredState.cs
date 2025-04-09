using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFiredState : State
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsPowerFired", true);
        Vector2 newPos = this.stateManager.transform.position;
        Quaternion newRot = this.stateManager.transform.rotation;
        this.stateManager.CharacterCtrl.EnergyShotSpawner.Spawn(this.stateManager.CharacterCtrl.EnergyShot, newPos, newRot);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.Fixedtime >= 0.5f)
        {
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsPowerFired", false);
    }
}
