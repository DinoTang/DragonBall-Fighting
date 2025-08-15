using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : BaseAirState
{
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.stateManager.mainStateType = new AirState();
        this.animator.SetBool("IsJump", true);
        this.stateManager.CharacterCtrl.CharacterDamageReceiver.ToggleIgnoreEnemy(true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (this.stateManager.CharacterCtrl.Rgb.velocity.y < 0)
        {
            this.animator.SetBool("IsFall", true);
            // this.animator.SetBool("IsJump", false);
        }
        if (this.stateManager.IsClone) return;
        if (InputManager.Instance.GetHitInput()) this.stateManager.SetNextState(new AirHit1State());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new AirKickState());
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new AirBlockState());
        if (InputManager.Instance.GetSuperDashInput() && this.stateManager.CanSuperDash)
            this.stateManager.SetNextState(new SuperDashState());

    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsJump", false);
        this.animator.SetBool("IsFall", false);
    }

}
