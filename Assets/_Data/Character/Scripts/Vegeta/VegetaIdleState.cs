using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaIdleState : IdleCombatState
{

    public override void OnUpdate()
    {
        if (InputManager.Instance.GetHorizontal() != 0) this.stateManager.SetNextState(new VegetaMoveState());
        if (InputManager.Instance.GetUpInput()) this.stateManager.SetNextState(new JumpState());
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new BlockState());
        if (InputManager.Instance.GetHitInput()) this.stateManager.SetNextState(new GroundHit1State());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new KickState());
        if (InputManager.Instance.GetChargeInput()) this.stateManager.SetNextState(new ChargeState());
        if (InputManager.Instance.GetLauncherInput()) this.stateManager.SetNextState(new LauncherState());
        if (InputManager.Instance.GetPowerFiredInput()) this.stateManager.SetNextState(new PowerFiredState());
    }
}
