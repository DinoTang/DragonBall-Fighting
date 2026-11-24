using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlockState : BaseAirState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsJump", true);
        this.animator.SetBool("IsBlock", true);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsBlock(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.ReleaseBlockHitInput(this.playerType))
        {
            this.BackMainState();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsBlock", false);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.SetIsBlock(false);
    }
}
