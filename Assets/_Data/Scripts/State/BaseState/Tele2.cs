using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Tele2 : CharacterState
{
    protected float distanceTele = 5f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("Tele2", true);
        this.stateManager.CharacterCtrl.CharacterCombo.SpawnEffectWhenTele();
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsImmortal(true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.timed >= 0.01f)
        {
            this.Teleing();
            this.stateManager.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("Tele2", false);
    }

    protected void Teleing()
    {
        // int dir = this.stateManager.CharacterCtrl.CharacterFlip.IsFacingRight ? -1 : 1;
        // Vector3 currentPos = this.stateManager.transform.position;
        // this.stateManager.transform.position = new Vector3(currentPos.x + dir * this.distanceTele, currentPos.y, currentPos.z);
        this.stateManager.CharacterCtrl.CharacterCombo.SpawnEffectWhenTele();
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsImmortal(false);
    }
}
