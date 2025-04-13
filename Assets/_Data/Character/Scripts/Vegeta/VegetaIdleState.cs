using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaIdleState : IdleCombatState
{
    
    public override void OnUpdate()
    {
        // this.stateManager.CharacterCtrl.CharacterFlip.Flipping();
        // if (this.stateManager.IsVegeta) return;
        if (InputManager.Instance.GetHorizontal() != 0) this.stateManager.SetNextState(new VegetaMoveState());
        // if (InputManager.Instance.GetNormalHitInput()) this.stateManager.SetNextState(new GroundHit1State());
        // if (InputManager.Instance.GetJumpInput()) this.stateManager.SetNextState(new JumpState());
        // if (InputManager.Instance.GetCrouchInput()) this.stateManager.SetNextState(new CrouchState());
        // if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new GroundStrongHitState());
        // if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new KickState());
        // if (InputManager.Instance.GetChargeInput()) this.stateManager.SetNextState(new ChargeState());
        // if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new BlockState());
        // if (InputManager.Instance.GetPowerFiredInput()) this.stateManager.SetNextState(new PowerFiredState());
    }
    
}
