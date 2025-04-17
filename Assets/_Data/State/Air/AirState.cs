using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : BaseAirState
{
    protected float jumpForce = 50f;
    public override void OnEnter(StateManager stateManager)
    {
        base.OnEnter(stateManager);
        this.animator.SetBool("IsJump", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (InputManager.Instance.GetHitInput()) this.stateManager.SetNextState(new AirHitState());
        if (InputManager.Instance.GetKickInput()) this.stateManager.SetNextState(new AirKickState());
        // if (InputManager.Instance.GetStrongHitInput()) this.stateManager.SetNextState(new AirStrongHitState());
        if (InputManager.Instance.GetBlockHitInput()) this.stateManager.SetNextState(new AirBlockState());
    }
    public override void OnExit()
    {
        base.OnExit();
        this.animator.SetBool("IsJump", false);
    }

}
